[Setup]
AppName=AutoClicker
AppVersion=1.0
DefaultDirName={autopf}\AutoClicker
DefaultGroupName=AutoClicker
OutputDir=.
OutputBaseFilename=AutoClickerInstaller
Compression=lzma
SolidCompression=yes

[Code]
function IsPythonInstalled: Boolean;
var
  ResultCode: Integer;
begin
  // Ejecuta 'python --version' y verifica si existe
  Result := Exec('cmd.exe', '/C python --version', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
end;

[Run]
; 1. Verificar si Python está instalado
Filename: "{cmd}"; Parameters: "/C python --version"; StatusMsg: "Verificando Python..."; Flags: runhidden waituntilterminated; Check: not IsPythonInstalled

; 2. Descargar Python si no está
Filename: "{cmd}"; Parameters: "/C powershell -Command ""Invoke-WebRequest -Uri 'https://www.python.org/ftp/python/3.10.0/python-3.10.0-amd64.exe' -OutFile '{tmp}\python-installer.exe'"""; StatusMsg: "Descargando Python..."; Flags: runhidden waituntilterminated; Check: not IsPythonInstalled
Filename: "{tmp}\python-installer.exe"; Parameters: "/quiet InstallAllUsers=1 PrependPath=1 Include_test=0"; StatusMsg: "Instalando Python..."; Flags: waituntilterminated; Check: not IsPythonInstalled

; 3. Descargar y descomprimir el ZIP del repo
Filename: "{cmd}"; Parameters: "/C powershell -Command ""Invoke-WebRequest -Uri 'https://github.com/tuusuario/tuapp/archive/refs/heads/main.zip' -OutFile '{tmp}\repo.zip'"""; StatusMsg: "Descargando AutoClicker..."; Flags: runhidden waituntilterminated

Filename: "{cmd}"; Parameters: "/C powershell -Command ""Expand-Archive -Path '{tmp}\repo.zip' -DestinationPath '{tmp}\repo' -Force"""; StatusMsg: "Descomprimiendo archivos..."; Flags: runhidden waituntilterminated

; 4. Copiar los archivos descomprimidos al directorio de instalación
Filename: "{cmd}"; Parameters: "/C xcopy /E /Y /I ""{tmp}\repo\tuapp-main"" ""{app}"""; StatusMsg: "Copiando archivos..."; Flags: runhidden waituntilterminated

; 5. Ejecutar run.py (que instala venv y dependencias)
Filename: "python"; Parameters: "run.py"; WorkingDir: "{app}"; Flags: nowait postinstall skipifsilent
