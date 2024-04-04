REM Bulk insert data into both the publication and subscription databases.
REM The BCP format depends on the snapshot format (native or character).
REM Execute at the command prompt.

bcp AdventureWorks2022..ProductTest in NewTable.bcp -T -SMYPUBLISHER n/c
bcp AdventureWorks2022Replica..ProductTest in NewTable.bcp -T -SMYPUBLISHER n/c