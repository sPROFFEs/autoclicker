#!/usr/bin/env python3
import os
import sys
import subprocess
import tkinter as tk
from tkinter import messagebox
import traceback

from utils import check_required_modules, install_missing_modules

VENV_DIR = "venv"

def is_running_in_venv():
    return os.path.basename(sys.prefix) == VENV_DIR or \
           os.path.exists(os.path.join(sys.prefix, "pyvenv.cfg"))

def create_virtualenv():
    try:
        subprocess.check_call([sys.executable, "-m", "venv", VENV_DIR])
        return True
    except Exception as e:
        print(f"Error creating virtualenv: {e}")
        return False

def install_deps_in_venv():
    python_bin = get_venv_python()
    try:
        subprocess.check_call([python_bin, "-m", "pip", "install", "--upgrade", "pip"])
        modules = ['pyautogui', 'keyboard', 'Pillow', 'pynput']
        subprocess.check_call([python_bin, "-m", "pip", "install"] + modules)
        return True
    except Exception as e:
        print(f"Error installing modules in venv: {e}")
        return False

def get_venv_python():
    if os.name == 'nt':
        return os.path.join(VENV_DIR, "Scripts", "python.exe")
    else:
        return os.path.join(VENV_DIR, "bin", "python")

def restart_in_venv():
    python_bin = get_venv_python()
    os.execv(python_bin, [python_bin] + sys.argv)

def create_launcher_bat():
    bat_path = os.path.abspath("launch_autoclicker.bat")
    venv_pythonw = os.path.abspath(os.path.join(VENV_DIR, "Scripts", "pythonw.exe"))
    script_path = os.path.abspath("run.py")

    bat_content = f'''@echo off
start "" "{venv_pythonw}" "{script_path}"
exit
'''

    with open(bat_path, "w") as f:
        f.write(bat_content)

    return bat_path

def import_or_install(module_name, package_name=None):
    package_name = package_name or module_name
    try:
        module = __import__(module_name)
        return module
    except ImportError:
        print(f"{module_name} no encontrado, instalando {package_name}...")
        python_bin = get_venv_python()
        subprocess.check_call([python_bin, "-m", "pip", "install", package_name])
        print(f"{package_name} instalado. Reiniciando script para aplicar cambios...")
        os.execv(python_bin, [python_bin] + sys.argv)


def create_windows_shortcut():
    # Intentamos importar pywin32 y sus componentes, o instalarlos si no existen
    pythoncom = import_or_install("pythoncom", "pywin32")
    from win32com.shell import shell, shellcon
    from win32com.client import Dispatch

    desktop = shell.SHGetFolderPath(0, shellcon.CSIDL_DESKTOP, None, 0)
    shortcut_path = os.path.join(desktop, "AutoClicker.lnk")

    if os.path.exists(shortcut_path):
        print("Shortcut already exists, skipping creation.")
        return False

    bat_path = create_launcher_bat()
    working_dir = os.path.abspath(".")

    shell_link = Dispatch("WScript.Shell").CreateShortcut(shortcut_path)
    shell_link.TargetPath = bat_path
    shell_link.WorkingDirectory = working_dir
    shell_link.IconLocation = os.path.abspath("icon.ico")
    shell_link.WindowStyle = 7  # Minimized
    shell_link.Description = "AutoClicker launcher"
    shell_link.Save()

    print(f"Shortcut created at {shortcut_path}")
    return True


def create_linux_shortcut():
    desktop = os.path.join(os.path.expanduser("~"), "Desktop")
    if not os.path.isdir(desktop):
        desktop = os.path.expanduser("~")

    shortcut_path = os.path.join(desktop, "AutoClicker.desktop")

    if os.path.exists(shortcut_path):
        print("Shortcut already exists, skipping creation.")
        return False

    python_path = os.path.abspath(get_venv_python())
    script_path = os.path.abspath("run.py")
    icon_path = os.path.abspath("icon.png")

    content = f"""[Desktop Entry]
Type=Application
Name=AutoClicker
Exec={python_path} {script_path}
Icon={icon_path}
Terminal=false
"""

    with open(shortcut_path, "w") as f:
        f.write(content)

    os.chmod(shortcut_path, 0o755)
    print(f"Shortcut created at {shortcut_path}")
    return True

def create_shortcut():
    if os.name == "nt":
        return create_windows_shortcut()
    elif os.name == "posix":
        return create_linux_shortcut()
    return False

def main():
    root = tk.Tk()
    root.withdraw()

    icon_path_win = os.path.abspath("icon.ico")
    icon_path_png = os.path.abspath("icon.png")

    try:
        if os.name == 'nt':
            root.iconbitmap(icon_path_win)
        else:
            icon = tk.PhotoImage(file=icon_path_png)
            root.iconphoto(False, icon)
    except Exception as e:
        print(f"No se pudo cargar el icono: {e}")

    if not is_running_in_venv():
        if not os.path.isdir(VENV_DIR):
            msg = ("No virtual environment found.\n\n"
                   "The application will create one now and install dependencies.\n\n"
                   "Continue?")
            if not messagebox.askyesno("Create Virtual Environment", msg):
                sys.exit(1)
            if not create_virtualenv():
                messagebox.showerror("Error", "Failed to create virtual environment.")
                sys.exit(1)

        if not install_deps_in_venv():
            messagebox.showerror("Error", "Failed to install dependencies in virtual environment.")
            sys.exit(1)

        try:
            created = create_shortcut()
            if created:
                messagebox.showinfo("Shortcut Created", "A desktop shortcut has been created for you.")
        except Exception as e:
            print(f"Failed to create shortcut: {e}")

        messagebox.showinfo("Restarting", "Restarting application inside virtual environment.")
        restart_in_venv()

    missing_modules = check_required_modules()
    if missing_modules:
        msg = f"The following required modules are missing:\n\n{', '.join(missing_modules)}\n\nWould you like to install them now?"
        if messagebox.askyesno("Missing Dependencies", msg):
            if not install_missing_modules(missing_modules):
                messagebox.showerror("Installation Failed",
                                     "Failed to install required modules manually, please use pip.")
                sys.exit(1)
            else:
                messagebox.showinfo("Installation Successful", "Modules installed successfully. Restarting...")
                os.execv(sys.executable, [sys.executable] + sys.argv)
        else:
            sys.exit(1)

    root.deiconify()

    try:
        from main import AutoClickerApp, Recorder, Player

        app = AutoClickerApp(root)
        app.recorder = Recorder(app)
        app.player = Player(app)

        app.start_recording = app.recorder.start_recording
        app.stop_recording = app.recorder.stop_recording
        app.start_playback = app.player.start_playback
        app.stop_playback = app.player.stop_playback

        root.mainloop()
    except Exception as e:
        err = traceback.format_exc()
        messagebox.showerror("Application Error", f"An unexpected error occurred:\n\n{e}\n\n{err}")
        print(err)
        sys.exit(1)

if __name__ == "__main__":
    main()