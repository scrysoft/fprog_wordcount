Group P - Patrick Friedel, Elmin Smajlovic

===== HOW TO USE =====

How to use our wordcount programm:
1. Replace <inputdirectory> with your filepath
2. Replace <outputdirectory> with the outputpath (eg. ..\output.txt)
3. Go to the outputpath and check the result


===== BATCHSCRIPT EXAMPLE =====

@ECHO OFF
cd Build
FPROG_wordcount.exe ..\sample-3mb-text-file.txt ..\output.txt
PAUSE
