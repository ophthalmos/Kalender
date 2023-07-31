#define MyAppName "Kalender"
#define MyAppVersion "2.0.3.0"
[Setup]
AppName={#MyAppName}
AppVerName={#MyAppName} {#MyAppVersion}
AppVersion={#MyAppVersion}
VersionInfoVersion={#MyAppVersion}
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=admin
AppPublisher=Wilhelm Happe
AppCopyright=© 2022 W. Happe
AppPublisherURL=https://www.ophthalmostar.de/
UsePreviousAppDir=yes
DefaultDirName={commonpf64}\{#MyAppName}
SetupIconFile={#MyAppName}.ico
DisableWelcomePage=yes
DisableDirPage=no
UninstallDisplayIcon={app}\{#MyAppName}.exe
DefaultGroupName={#MyAppName}
OutputDir=.
OutputBaseFilename={#MyAppName}Setup
SolidCompression=yes
DirExistsWarning=no
MinVersion=0,6.0
CloseApplications=force
AppMutex={#MyAppName}_MutiStartPrevent
SignTool=sha256
InfoAfterFile=Liesmich.txt

[Files]
Source: "bin\x64\Release\{#MyAppName}.exe"; DestDir: "{app}"; Permissions: users-modify; Flags: replacesameversion
Source: "bin\x64\Release\{#MyAppName}.exe.config"; DestDir: "{app}"; Permissions: users-modify; Flags: ignoreversion
Source: "{#MyAppName}.ico"; DestDir: "{app}"; Permissions: users-modify;
Source: "Lizenzvereinbarung.txt"; DestDir: "{app}"; Permissions: users-modify;

[Icons]
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppName}.exe";
Name: "{group}\{#MyAppName} starten"; Filename: "{app}\{#MyAppName}.exe"
Name: "{group}\{#MyAppName} von diesem PC entfernen"; Filename: "{uninstallexe}"

[UninstallDelete]
Type: filesandordirs; Name: {userappdata}\{#MyAppName}\{#MyAppName}*
Type: dirifempty; Name: {userappdata}\{#MyAppName}

[Languages]
Name: "de"; MessagesFile: "compiler:languages\German.isl"; LicenseFile: "Lizenzvereinbarung.txt"

[Registry]
Root: HKCU; Subkey: "Software\{#MyAppName}"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\{#MyAppName}"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}"
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "{#MyAppName}"; Flags: dontcreatekey uninsdeletevalue

[Run]
Filename: "schtasks"; Parameters: "/Create /F /RL HIGHEST /SC ONLOGON /DELAY 0000:20 /TN ""{#MyAppName}"" /TR ""'{app}\{#MyAppName}.exe'"""; Flags: runhidden
Filename: {app}\{#MyAppName}.exe; Verb: runas; Description: "{#MyAppName} starten" ; Flags: nowait postinstall skipifsilent shellexec

[UninstallRun]
Filename: "schtasks"; Parameters: "/Delete /F  /TN ""{#MyAppName}"""; Flags: runhidden

[Messages]
BeveledLabel=
de.WinVersionTooLowError=Dieses Programm erfordert Windows Vista oder höher.
de.ConfirmUninstall=Sind Sie sicher, dass Sie %1 und alle zugehörigen Komponenten sowie Einstellungen entfernen möchten? Eine Deinstallation ist vor einem Update nicht erforderlich.
