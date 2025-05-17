[Setup]
AppName=PyClickRecorder
AppVersion=1.0
DefaultDirName={localappdata}\AutoClicker
DefaultGroupName=AutoClicker
OutputDir=.
OutputBaseFilename=PyClickRecorder
Compression=lzma
SolidCompression=yes
UsePreviousAppDir=no
SetupIconFile=installer_icon.ico


[Run]
; Step 1: Download Python
Filename: "powershell.exe"; \
Parameters: "-ExecutionPolicy Bypass -Command ""Invoke-WebRequest -Uri 'https://www.python.org/ftp/python/3.12.10/python-3.12.10-amd64.exe ' -OutFile '{tmp}\\python-installer.exe'"""; \
StatusMsg: "Downloading Python installer..."; \
Flags: runhidden waituntilterminated

; Step 2: Run Python installer
Filename: "{tmp}\\python-installer.exe"; \
Description: "Install Python before proceeding"; \
StatusMsg: "Installing Python..."; \
Flags: postinstall

; Step 3: Download repository
Filename: "powershell.exe"; \
Parameters: "-ExecutionPolicy Bypass -Command ""Invoke-WebRequest -Uri 'https://codeload.github.com/sPROFFEs/autoclicker/zip/refs/heads/main ' -OutFile '{tmp}\\repo.zip'"""; \
StatusMsg: "Downloading AutoClicker files..."; \
Flags: runhidden waituntilterminated

; Step 4: Extract files
Filename: "powershell.exe"; \
Parameters: "-ExecutionPolicy Bypass -Command ""Expand-Archive -Path '{tmp}\\repo.zip' -DestinationPath '{tmp}\\repo' -Force"""; \
StatusMsg: "Extracting files..."; \
Flags: runhidden waituntilterminated

; Step 5: Copy files to final directory
Filename: "{cmd}"; \
Parameters: "/C xcopy /E /Y /I ""{tmp}\\repo\\autoclicker-main"" ""{app}"""; \
StatusMsg: "Copying files..."; \
Flags: runhidden waituntilterminated

; Step 6: Open folder after installation
Filename: "explorer.exe"; \
Parameters: """{app}"""; \
Description: "Open program folder"; \
Flags: postinstall shellexec nowait

[Code]
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    MsgBox(
      'Python has been installed. Be sure to complete the installation before using AutoClicker.'#13#10#10 +
      'You can now run the run.py file from the program folder.',
      mbInformation, MB_OK);
  end;
end;