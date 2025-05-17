#!/bin/bash

set -e

APP_NAME="AutoClicker"
INSTALL_DIR="$HOME/.local/share/$APP_NAME"
WRAPPER_PATH="/usr/local/bin/autoclicker-launcher"
POLICY_FILE="/usr/share/polkit-1/actions/com.autoclicker.pkexec.policy"
DESKTOP_SHORTCUT="$HOME/Desktop/AutoClicker.desktop"
ICON_PATH="$INSTALL_DIR/icon.png"

echo "[*] Installing requirements..."
sudo apt update && sudo apt install python3 python3-venv unzip wget -y

echo "[*] Downloading $APP_NAME..."
wget -qO /tmp/repo.zip https://codeload.github.com/sPROFFEs/autoclicker/zip/refs/heads/main
unzip -o /tmp/repo.zip -d /tmp/

echo "[*] Installing to $INSTALL_DIR..."
mkdir -p "$INSTALL_DIR"
cp -r /tmp/autoclicker-main/* "$INSTALL_DIR"

cd "$INSTALL_DIR"

echo "[*] Creating wrapper script in $WRAPPER_PATH..."
sudo bash -c "cat > $WRAPPER_PATH" <<EOF
#!/bin/bash
export DISPLAY=\$DISPLAY
export XAUTHORITY=\$XAUTHORITY
cd "$INSTALL_DIR"
exec "$INSTALL_DIR/venv/bin/python" run.py
EOF

sudo chmod +x "$WRAPPER_PATH"

echo "[*] Creating polkit policy in $POLICY_FILE..."
sudo bash -c "cat > $POLICY_FILE" <<EOF
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE policyconfig PUBLIC
  "-//freedesktop//DTD PolicyKit Policy Configuration 1.0//EN"
  "http://www.freedesktop.org/standards/PolicyKit/1/policyconfig.dtd">
<policyconfig>
  <action id="com.autoclicker.pkexec">
    <description>Run AutoClicker as root</description>
    <message>Authentication is required to run AutoClicker</message>
    <defaults>
      <allow_any>auth_admin_keep</allow_any>
      <allow_inactive>auth_admin_keep</allow_inactive>
      <allow_active>auth_admin_keep</allow_active>
    </defaults>
    <annotate key="org.freedesktop.policykit.exec.path">$WRAPPER_PATH</annotate>
    <annotate key="org.freedesktop.policykit.exec.allow_gui">true</annotate>
  </action>
</policyconfig>
EOF

echo "[*] Creating desktop shortcut..."
cat > "$DESKTOP_SHORTCUT" <<EOF
[Desktop Entry]
Type=Application
Name=AutoClicker
Exec=pkexec $WRAPPER_PATH
Icon=$ICON_PATH
Terminal=false
EOF

chmod +x "$DESKTOP_SHORTCUT"

echo "[*] Running app to create virtualenv and finish setup..."
python3 run.py

echo "[✓] Installation complete. Shortcut created on your Desktop."
