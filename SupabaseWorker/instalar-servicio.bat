@echo off
SET "DIR=%~dp0"
echo Instalando desde: %DIR%
IF NOT EXIST "%DIR%SupabaseWorker.exe" (
    echo ERROR: No se encontro SupabaseWorker.exe
    pause
    exit /b 1
)
schtasks /Delete /TN "SupabaseWorker\Periodico" /F >nul 2>&1
schtasks /Delete /TN "SupabaseWorker\AlIniciar" /F >nul 2>&1
schtasks /Create /TN "SupabaseWorker\Periodico" /TR "\"%DIR%SupabaseWorker.exe\"" /SC MINUTE /MO 10 /RU "%USERNAME%" /F
schtasks /Create /TN "SupabaseWorker\AlIniciar" /TR "\"%DIR%SupabaseWorker.exe\"" /SC ONSTART /DELAY 0000:30 /RU "%USERNAME%" /F
echo.
echo Listo!
schtasks /Query /TN "SupabaseWorker\Periodico"
pause