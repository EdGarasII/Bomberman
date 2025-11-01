@echo off
echo Starting Bomberman Multiplayer Test...
echo.
echo This will open 2 game instances for testing multiplayer on the same PC
echo.
echo First instance will HOST, second will JOIN
echo.
pause

echo Building game...
dotnet build

if %ERRORLEVEL% NEQ 0 (
    echo Build failed!
    pause
    exit /b 1
)

echo.
echo Starting HOST instance...
start "Bomberman - Host" dotnet run

timeout /t 3 /nobreak >nul

echo Starting CLIENT instance...
start "Bomberman - Client" dotnet run

echo.
echo Both instances should be opening now!
echo In the first window: Click "Multiplayer" -> "Host Game"
echo In the second window: Click "Multiplayer" -> "Join Game" (keep localhost)
echo.
pause


