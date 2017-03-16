REM Bulk insert data into both the publication and subscription databases.
REM The BCP format depends on the snapshot format (native or character).
REM Execute at the command prompt.

bcp AdventureWorks2012..ProductTest in NewTable.bcp -T -SMYPUBLISHER n/c
bcp AdventureWorks2012Replica..ProductTest in NewTable.bcp -T -SMYPUBLISHER n/c