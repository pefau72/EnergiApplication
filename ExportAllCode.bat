@echo off
powershell -NoLogo -NoProfile -Command ^
  "Get-ChildItem -Recurse -Filter *.cs | ^
   Sort-Object FullName | ^
   ForEach-Object { '===== FILE: ' + $_.FullName + ' ====='; Get-Content -LiteralPath $_.FullName; ''; } | ^
   Out-File AllCode.txt"
echo Done! File generated: AllCode.txt
pause
