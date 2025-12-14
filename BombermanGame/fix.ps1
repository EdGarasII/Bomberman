$lines = @(Get-Content GameForm.cs)
$lines[1523] = "            }"
$newLines = @()
for ($i = 0; $i -lt $lines.Length; $i++) {
    $newLines += $lines[$i]
    if ($i -eq 1523) {
        $newLines += "        }"
    }
}
$newLines | Set-Content GameForm.cs
