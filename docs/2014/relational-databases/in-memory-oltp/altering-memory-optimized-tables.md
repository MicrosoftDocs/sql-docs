---
title: "Altering Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 690b70b7-5be1-4014-af97-54e531997839
author: MightyPen
ms.author: genemi
manager: craigg
---
# Altering Memory-Optimized Tables
  Performing ALTER operations on memory-optimized tables is not supported. This includes such operations as changing the bucket_count, adding or removing an index, and adding or removing a column. This topic provides guidelines on how to update memory-optimized tables.  
  
## Updating the Definition of a Memory-Optimized Table  
 Updating the definition of a memory-optimized table requires you to create a new table with the updated table definition, copy the data to the new table, and start using the new table. Unless the table is read-only, this requires stopping the workload on the table, to ensure no changes are made to the table while the data copy is performed.  
  
 The following procedure outlines the steps required to update a table. In this example, the update adds an index. This procedure preserves the name of the table and requires two data copy operations: once to a temporary table, and once to the new table. Changing the bucket_count of an index or adding or removing a column is performed the same way.  
  
1.  Stop the workload on the table.  
  
2.  Generate script for the table and add the new index to the script.  
  
3.  Generate script for the schema-bound objects (mainly natively compiled stored procedures) referencing T and their permissions.  
  
     The schema-bound objects referencing the table can be found using the following query:  
  
    ```tsql  
    declare @t nvarchar(255) = N'<table name>'  
  
    select r.referencing_schema_name, r.referencing_entity_name  
    from sys.dm_sql_referencing_entities (@t, 'OBJECT') as r join sys.sql_modules m on r.referencing_id=m.object_id  
    where r.is_caller_dependent = 0 and m.is_schema_bound=1;  
    ```  
  
     The permissions of a stored procedure can be scripted using the following [!INCLUDE[tsql](../../includes/tsql-md.md)]:  
  
    ```tsql  
    declare @sp nvarchar(255) = N'<procedure name>'  
    declare @permissions nvarchar(max) = N''  
  
    select @permissions += dp.state_desc + N' ' + dp.permission_name + N' ON ' +   
       quotename(schema_name(o.schema_id)) + N'.' + quotename(o.name) + N' TO ' +  
       quotename(u.name) + N'; ' + char(13)  
    from sys.database_permissions as dp  
  
    join sys.database_principals as u  
       on u.principal_id = dp.grantee_principal_id  
  
    join sys.objects as o  
       on o.object_id = dp.major_id  
    where dp.class = 1 /* object */  
       and dp.minor_id = 0 and o.object_id=object_id(@sp);  
  
    select @permissions  
    ```  
  
4.  Create a copy of the table and copy the data from the original table to the copy of the table. The copy can be created using the following [!INCLUDE[tsql](../../includes/tsql-md.md)]<sup>1</sup>.  
  
    ```tsql  
    select * into dbo.T_copy from dbo.T  
    ```  
  
     If there is enough available memory, `T_copy` could be a memory-optimized table, which makes the data copy faster.<sup>2</sup>  
  
5.  Drop the schema-bound objects referencing the original table.  
  
6.  Drop the original table.  
  
7.  Create the new table (`T`) with the script containing the new index.  
  
8.  Copy the data from `T_copy` to `T`.  
  
9. Recreate the referencing schema-bound objects and apply the permissions.  
  
10. Start the workload on `T`.  
  
 <sup>1</sup> Note that `T_copy` is persisted to disk in this example. If a backup of `T` is available, `T_copy` could be a temporary or a non-durable table.  
  
 <sup>2</sup> There must be enough memory for `T_copy`. Memory is not freed immediately on `DROP TABLE`. If `T_copy` is memory optimized, there needs to be enough memory for two additional copies of `T`. If `T_copy` is a disk-based table, there only needs to be enough memory for one additional copy of `T`, due to the garbage collector needing to catch up after dropping the old version of `T`.  
  
## Changing Schema (PowerShell)  
 The following PowerShell scripts prepare and generate for schema changes by scripting the table and associated permissions.  
  
 Usage: prepare_schema_change.ps1 *server_name**db_name`schema_name`table_name*  
  
 This script takes as arguments a table, and scripts the object and its permissions and referencing schema-bound objects and their permissions in the current folder. A total of 7 scripts are generated for updating the schema of the input table:  
  
-   Copy data to a temporary table (a heap).  
  
-   Drop schema-bound objects referencing the table.  
  
-   Drop the table.  
  
-   Recreate the table with the new schema and reapply permissions.  
  
-   Copy data from the temporary table to the recreated table.  
  
-   Recreate schema-bound objects referencing the table and their permissions.  
  
-   Drop the temporary table.  
  
 The script for step 4 should be updated to reflect the desired schema changes. If there are any changes in the columns of the table, the scripts for steps 5 (copy data from temporary table) and 6 (recreate stored procedures) should be updated as necessary.  
  
```tsql  
# Prepare for schema changes by scripting out the table, as well as associated permissions  
# --------  
# Usage: prepare_schema_change.ps1 server_name db_name schema_name table_name  
# stop execution once an error occurs  
$ErrorActionPreference="Stop"  
  
if($args.Count -le 3)  
{  
   throw "Usage prepare_schema_change.ps1 server_name db_name schema_name table_name"  
}  
  
$servername = $args[0]  
$database = $args[1]  
$schema = $args[2]  
$object = $args[3]  
  
$object_heap = "$object$(Get-Random)"  
  
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.SMO") | out-null  
  
$server =  New-Object ("Microsoft.SqlServer.Management.SMO.Server") ($servername)  
$scripter = New-Object ("Microsoft.SqlServer.Management.SMO.Scripter") ($server)  
  
## initialize table variable  
$tableUrn = $server.Databases[$database].Tables[$object, $schema]  
if($tableUrn.Count -eq 0)  
{  
   throw "Table or database not found"  
}  
  
## initialize scripting object  
$scriptingOptions = New-Object ("Microsoft.SqlServer.Management.SMO.ScriptingOptions")   
$scriptingOptions.Permissions = $True  
$scriptingOptions.ScriptDrops = $True  
  
$scripter.Options = $scriptingOptions;  
  
write-host "(1) Scripting SELECT INTO $object_heap for table [$object] to 1_copy_to_heap_for_$schema`_$object.sql"  
Echo "SELECT * INTO $schema.$object_heap FROM $schema.$object WITH (SNAPSHOT)" | Out-File "1_copy_to_heap_$schema`_$object.sql";  
write-host "--done--"  
write-host ""  
  
write-host "(2) Scripting DROP for procs schema-bound to [$object] 2_drop_procs_$schema`_$object.sql"  
## query referencing schema-bound objects  
$dt = $server.Databases[$database].ExecuteWithResults("select r.referencing_schema_name, r.referencing_entity_name  
from sys.dm_sql_referencing_entities ('$schema.$object', 'OBJECT') as r join sys.sql_modules m on r.referencing_id=m.object_id  
where r.is_caller_dependent = 0 and m.is_schema_bound=1;")  
  
## initialize out file  
Echo "" | Out-File "2_drop_procs_$schema`_$object.sql"  
## loop through schema-bound objects  
Foreach ($t in $dt.Tables)  
{    
   Foreach ($r in $t.Rows)  
   {    
      ## script object   
      $so =  $server.Databases[$database].StoredProcedures[$r[1], $r[0]]  
      $scripter.Script($so) | Out-File -Append "2_drop_procs_$schema`_$object.sql"  
   }  
}  
write-host "--done--"  
write-host ""  
  
write-host "(3) Scripting DROP table for [$object] to 3_drop_table_$schema`_$object.sql"  
$scripter.Script($tableUrn) | Out-File "3_drop_table_$schema`_$object.sql";  
write-host "--done--"  
write-host ""  
  
## now script creates  
$scriptingOptions.ScriptDrops = $False  
  
write-host "(4) Scripting CREATE table and permissions for [$object] to !please_edit_4_create_table_$schema`_$object.sql"  
write-host "***** rename this script to 4_create_table.sql after completing the updates to the schema"  
$scripter.Script($tableUrn) | Out-File "!please_edit_4_create_table_$schema`_$object.sql";  
write-host "--done--"  
write-host ""  
  
write-host "(5) Scripting INSERT INTO table from heap and UPDATE STATISTICS for [$object] to 5_copy_from_heap_$schema`_$object.sql"  
write-host "[update this script if columns are added to or removed from the table]"  
Echo "INSERT INTO [$schema].[$object] SELECT * FROM [$schema].[$object_heap]; UPDATE STATISTICS [$schema].[$object] WITH FULLSCAN, NORECOMPUTE" | Out-File "5_copy_from_heap_$schema`_$object.sql";  
write-host "--done--"  
write-host ""  
  
write-host "(6) Scripting CREATE PROC and permissions for procedures schema-bound to [$object] to 6_create_procs_$schema`_$object.sql"  
write-host "[update the procedure definitions if columns are renamed or removed]"  
## initialize out file  
Echo "" | Out-File "6_create_procs_$schema`_$object.sql"  
## loop through schema-bound objects  
Foreach ($t in $dt.Tables)  
{    
   Foreach ($r in $t.Rows)  
   {    
      ## script the schema-bound object  
      $so =  $server.Databases[$database].StoredProcedures[$r[1], $r[0]]  
      foreach($s in $scripter.Script($so))  
        {  
            Echo $s | Out-File -Append "6_create_procs_$schema`_$object.sql"  
            Echo "GO" | Out-File -Append "6_create_procs_$schema`_$object.sql"  
        }  
   }  
}  
write-host "--done--"  
write-host ""  
  
write-host "(7) Scripting DROP $object_heap to 7_drop_heap_$schema`_$object.sql"  
Echo "DROP TABLE $schema.$object_heap" | Out-File "7_drop_heap_$schema`_$object.sql";  
write-host "--done--"  
write-host ""  
  
```  
  
 The following PowerShell script executes the schema changes that were scripted in the previous sample. This script takes as argument a table, and executes the schema change scripts that were generated for that table and the associated stored procedures.  
  
 Usage: execute_schema_change.ps1 *server_name**db_name`schema_name`table_name*  
  
```tsql  
# stop execution once an error occurs  
$ErrorActionPreference="Stop"  
  
if($args.Count -le 3)  
{  
   throw "Usage execute_schema_change.ps1 server_name db_name schema_name table_name"  
}  
  
$servername = $args[0]  
$database = $args[1]  
$schema = $args[2]  
$object = $args[3]  
  
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.SMO") | out-null  
  
$server =  New-Object ("Microsoft.SqlServer.Management.SMO.Server") ($servername)  
$database = $server.Databases[$database]  
$table = $database.Tables[$object, $schema]  
if($table.Count -eq 0)  
{  
   throw "Table or database not found"  
}  
  
$1 = Get-Item "1_copy_to_heap_$schema`_$object.sql"  
$2 = Get-Item "2_drop_procs_$schema`_$object.sql"  
$3 = Get-Item "3_drop_table_$schema`_$object.sql"  
$4 = Get-Item "4_create_table_$schema`_$object.sql"  
$5 = Get-Item "5_copy_from_heap_$schema`_$object.sql"  
$6 = Get-Item "6_create_procs_$schema`_$object.sql"  
$7 = Get-Item "7_drop_heap_$schema`_$object.sql"  
  
write-host "(1) Running SELECT INTO heap for table [$object] from 1_copy_to_heap_for_$schema`_$object.sql"  
$database.ExecuteNonQuery("$(Echo $1.OpenText().ReadToEnd())")  
write-host "--done--"  
write-host ""  
  
write-host "(2) Running DROP for procs schema-bound from [$object] 2_drop_procs_$schema`_$object.sql"  
$database.ExecuteNonQuery("$(Echo $2.OpenText().ReadToEnd())")  
write-host "--done--"  
write-host ""  
  
write-host "(3) Running DROP table for [$object] to 4_drop_table_$schema`_$object.sql"  
$database.ExecuteNonQuery("$(Echo $3.OpenText().ReadToEnd())")  
write-host "--done--"  
write-host ""  
  
write-host "(4) Running CREATE table and permissions for [$object] from 4_create_table_$schema`_$object.sql"  
$database.ExecuteNonQuery("$(Echo $4.OpenText().ReadToEnd())")  
write-host "--done--"  
write-host ""  
  
write-host "(5) Running INSERT INTO table from heap for [$object] and UPDATE STATISTICS from 5_copy_from_heap_$schema`_$object.sql"  
$database.ExecuteNonQuery("$(Echo $5.OpenText().ReadToEnd())")  
write-host "--done--"  
write-host ""  
  
write-host "(6) Running CREATE PROC and permissions for procedures schema-bound to [$object] from 6_create_procs_$schema`_$object.sql"  
$database.ExecuteNonQuery("$(Echo $6.OpenText().ReadToEnd())")  
write-host "--done--"  
write-host ""  
  
write-host "(7) Running DROP heap from 7_drop_heap_$schema`_$object.sql"  
$database.ExecuteNonQuery("$(Echo $7.OpenText().ReadToEnd())")  
write-host "--done--"  
write-host ""  
```  
  
## See Also  
 [Memory-Optimized Tables](memory-optimized-tables.md)  
  
  
