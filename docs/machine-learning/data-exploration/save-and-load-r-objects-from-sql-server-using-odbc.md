---
title: Save and load R objects using ODBC
description: The RevoScaleR package includes serialization and deserialization functions that greatly improve performance, and store the object more compactly.
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 04/27/2021
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: contperf-fy21q4
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Use ODBC to save and load R objects in SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

Learn how to use the **RevoScaleR** package to store serialized R objects in a table and then load the object from the table as needed with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md). This can be used when training and saving a model, and then use it later for scoring or analysis.

## RevoScaleR package

The **RevoScaleR** package includes serialization and deserialization functions that can R objects compactly to SQL Server and then read the objects from the table. In general, each function call uses a simple key value store, in which the key is the name of the object, and the value associated with the key is the varbinary R object to be moved in or out of a table.

To save R objects to SQL Server directly from an R environment, you must:

+ established a connection to SQL Server using the *RxOdbcData* data source.
+ Call the new functions over the ODBC connection
+ Optionally, you can specify that the object not be serialized. Then, choose a new compression algorithm to use instead of the default compression algorithm.

By default, any object that you call from R to move to SQL Server is serialized and compressed. Conversely, when you load an object from a SQL Server table to use in your R code, the object is deserialized and decompressed.

## List of new functions

- `rxWriteObject` writes an R object into SQL Server using the ODBC data source.

- `rxReadObject` reads an R object from a SQL Server database, using an ODBC data source

- `rxDeleteObject` deletes an R object from the SQL Server database specified in the ODBC data source. If there are multiple objects identified by the key/version combination, all are deleted.

- `rxListKeys` lists as key-value pairs all the available objects. This helps you determine the names and versions of the R objects.

For detailed help on the syntax of each function, use R help. Details are also available in the [ScaleR reference](/machine-learning-server/r-reference/revoscaler/revoscaler).

## How to store R objects in SQL Server using ODBC

This procedure demonstrates how you can use the new functions to create a model and save it to SQL Server.

1. Set up the connection string for the SQL Server.
   ```R
   conStr <- 'Driver={SQL Server};Server=localhost;Database=storedb;Trusted_Connection=true'
   ```
2. Create an *rxOdbcData* data source object in R using the connection string.
   ```R
   ds <- RxOdbcData(table="robjects", connectionString=conStr)
   ```

3. Delete the table if it already exists, and you don't want to track old versions of the objects.

   ```R
   if(rxSqlServerTableExists(ds@table, ds@connectionString)) {
       rxSqlServerDropTable(ds@table, ds@connectionString)
       }
   ```
   
4. Define a table that can be used to store binary objects.

   ```R
   ddl <- paste(" CREATE TABLE [", ds@table, "] 
      (","  [id] varchar(200) NOT NULL,
       "," [value] varbinary(max),
       "," CONSTRAINT unique_id UNIQUE (id))", 
       sep = "") 
   ```
5. Open the ODBC connection to create the table, and when the DDL statement has completed, close the connection.

   ```R
    rxOpen(ds, "w") 
    rxExecuteSQLDDL(ds, ddl) 
    rxClose(ds)
    ```
6. Generate the R objects that you want to store.

   ```R
   infertLogit <- rxLogit(case ~ age + parity + education + spontaneous + induced, 
     data = infert)
   ```
6. Use the *RxOdbcData* object created earlier to save the model to the database.

   ```R
   rxWriteObject(ds, "logit.model", infertLogit)
   ```

## How to read R objects from SQL Server using ODBC

This procedure demonstrates how you can use the new functions to load a model from SQL Server.

1. Set up the connection string for the SQL Server.

   ```R
   conStr2 <- 'Driver={SQL Server};Server=localhost;Database=storedb;Trusted_Connection=true'
   ```
2. Create an *rxOdbcData* data source object in R, using the connection string.

   ```R
   ds <- RxOdbcData(table="robjects", connectionString=conStr2)
   ```
3. Read the model from the table by specifying its R object name.

   ```R
    infertLogit2 <- rxReadObject(ds, "logit.model")
   ```

## Next steps

+ [What is SQL Server Machine Learning Services?](../sql-server-machine-learning-services.md)
