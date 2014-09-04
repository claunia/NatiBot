; Script generated with the Venis Install Wizard

; Define your application name
!define APPNAME "NatiBot"
!define APPNAMEANDVERSION "NatiBot 1.0"

; Main Install settings
Name "${APPNAMEANDVERSION}"
InstallDir "$PROGRAMFILES\NatiBot"
InstallDirRegKey HKLM "Software\${APPNAME}" ""
OutFile "NatiBot 1.0.exe"

; Use compression
SetCompressor /SOLID LZMA
SetCompressorDictSize 16

; Modern interface settings
!include "MUI2.nsh"

!define MUI_ABORTWARNING
!define MUI_FINISHPAGE_RUN "$INSTDIR\NatiBot.exe"

; MUI Settings
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP "InstallHeader.bmp" ; optional
!define MUI_WELCOMEFINISHPAGE_BITMAP "InstallWelcome.bmp"

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "..\LICENSE.TXT"
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

; Set languages (first is default language)
!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_LANGUAGE "Catalan"
!insertmacro MUI_LANGUAGE "Spanish"
!insertmacro MUI_RESERVEFILE_LANGDLL

XPStyle on

!define PRODUCT_MAJOR 1
!define PRODUCT_MINOR 0
!define PRODUCT_BUILD 325
!define PRODUCT_NAME "NatiBot"
!define PRODUCT_VERSION "$PRODUCT_MAJOR.$PRODUCT_MINOR.$PRODUCT_BUILD"
!define PRODUCT_PUBLISHER "Claunia.com"
!define PRODUCT_WEB_SITE "http://www.natibot.com/"

VIProductVersion "1.0.0.325"
VIAddVersionKey "ProductName" "NatiBot"
VIAddVersionKey "Comments" ""
VIAddVersionKey "CompanyName" "Claunia.com"
VIAddVersionKey "LegalTrademarks" "See License.txt for licensing terms"
VIAddVersionKey "LegalCopyright" "© Claunia.com"
VIAddVersionKey "FileDescription" "NatiBot Installer"
VIAddVersionKey "FileVersion" "1.0.0"

Section "NatiBot" Section1

	; Set Section properties
	SetOverwrite ifnewer

	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\"
	File "..\bin\NatiBot.exe"
	File "..\bin\openjpeg-dotnet.dll"
	File "..\bin\openjpeg-dotnet-x86_64.dll"
	CreateShortCut "$DESKTOP\NatiBot.lnk" "$INSTDIR\NatiBot.exe"
	CreateDirectory "$SMPROGRAMS\Claunia.com\NatiBot"
	CreateShortCut "$SMPROGRAMS\Claunia.com\NatiBot\NatiBot.lnk" "$INSTDIR\NatiBot.exe"
	CreateShortCut "$SMPROGRAMS\Claunia.com\NatiBot\Uninstall.lnk" "$INSTDIR\uninstall.exe"
	WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\Claunia.com\NatiBot\NatiBot Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
SectionEnd

Section -FinishSection

	WriteRegStr HKLM "Software\${APPNAME}" "" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayName" "${APPNAME}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "UninstallString" "$INSTDIR\uninstall.exe"
	WriteUninstaller "$INSTDIR\uninstall.exe"

SectionEnd

; Modern install component descriptions
!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
	!insertmacro MUI_DESCRIPTION_TEXT ${Section1} ""
!insertmacro MUI_FUNCTION_DESCRIPTION_END

;Uninstall section
Section Uninstall

	;Remove from registry...
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}"
	DeleteRegKey HKLM "SOFTWARE\${APPNAME}"

	; Delete self
	Delete "$INSTDIR\uninstall.exe"

	; Delete Shortcuts
	Delete "$DESKTOP\NatiBot.lnk"
	Delete "$SMPROGRAMS\Claunia.com\NatiBot\NatiBot.lnk"
	Delete "$SMPROGRAMS\Claunia.com\NatiBot\Uninstall.lnk"
	Delete "$SMPROGRAMS\Claunia.com\NatiBot\NatiBot Website.lnk"

	; Clean up NatiBot
	Delete "$INSTDIR\NatiBot.exe"
  Delete "$INSTDIR\openjpeg-dotnet.dll"
	Delete "$INSTDIR\openjpeg-dotnet-x86_64.dll"

	; Remove remaining directories
	RMDir "$SMPROGRAMS\claunia.com\NatiBot"
	RMDir "$INSTDIR\"

SectionEnd

; On initialization
Function .onInit

	!insertmacro MUI_LANGDLL_DISPLAY

FunctionEnd

BrandingText "Claunia.com"

; eof