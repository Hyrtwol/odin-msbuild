@echo off
@rem select code page with utf-8 support CP_UTF8
chcp 65001 > NUL 2> NUL

if "%VSCMD_ARG_TGT_ARCH%"=="" call "%ProgramFiles%\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

@echo on
dotnet clean -c Release
dotnet build -c Release
dotnet test -c Release
@echo off

:done
