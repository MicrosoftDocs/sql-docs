---
title: "Import JSON documents into SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: 01/19/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: 0e908ec0-7173-4cd2-8f48-2700757b53a5
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Import JSON documents into SQL Server

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

This topic describes how to import JSON files into SQL Server. Currently there are lots of JSON documents stored in files. Applications log information in JSON files, sensors generate information that's stored in JSON files, and so forth. It's important to be able to read the JSON data stored in files, load the data into SQL Server, and analyze it.

## Import a JSON document into a single column

**OPENROWSET(BULK)** is a table-valued function that can read data from any file on the local drive or network, if SQL Server has read access to that location. It returns a table with a single column that contains the contents of the file. There are various options that you can use with the OPENROWSET(BULK) function, such as separators. But in the simplest case, you can just load the entire contents of a file as a text value. (This single large value is known as a single character large object, or SINGLE_CLOB.) 

Here's an example of the **OPENROWSET(BULK)** function that reads the contents of a JSON file and returns it to the user as a single value:

```sql
SELECT BulkColumn
 FROM OPENROWSET (BULK 'C:\JSON\Books\book.json', SINGLE_CLOB) as j;
```

OPENJSON(BULK) reads the content of the file and returns it in `BulkColumn`.

You can also load the contents of the file into a local variable or into a table, as shown in the following example:

```sql
-- Load file contents into a variable
SELECT @json = BulkColumn
 FROM OPENROWSET (BULK 'C:\JSON\Books\book.json', SINGLE_CLOB) as j

-- Load file contents into a table 
SELECT BulkColumn
 INTO #temp 
 FROM OPENROWSET (BULK 'C:\JSON\Books\book.json', SINGLE_CLOB) as j
```

After loading the contents of the JSON file, you can save the JSON text in a table.

## Import multiple JSON documents

You can use the same approach to load a set of JSON files from the file system into a local variable one at a time. Assume that the files are named `book<index>.json`.
  
```sql
DECLARE @i INT = 1
DECLARE @json AS NVARCHAR(MAX)

WHILE(@i < 10)
BEGIN
    SET @file = 'C:\JSON\Books\book' + cast(@i AS VARCHAR(5)) + '.json';
    SELECT @json = BulkColumn FROM OPENROWSET (BULK (@file), SINGLE_CLOB) AS j
    SELECT * FROM OPENJSON(@json) AS json
    -- Optionally, save the JSON text in a table.
    SET @i = @i + 1 ;
END
```

## Import JSON documents from Azure File Storage

You can also use OPENROWSET(BULK) as described above to read JSON files from other file locations that SQL Server can access. For example, Azure File Storage supports the SMB protocol. As a result you can map a local virtual drive to the Azure File storage share using the following procedure:

1.  Create a file storage account (for example, `mystorage`), a file share (for example, `sharejson`), and a folder in Azure File Storage by using the Azure portal or Azure PowerShell.
2.  Upload some JSON files to the file storage share.
3.  Create an outbound firewall rule in Windows Firewall on your computer that allows port 445. Note that your Internet service provider may block this port. If you get a DNS error (error 53) in the following step, then you have not opened port 445, or your ISP is blocking it.
4. Mount the Azure File Storage share as a local drive (for example `T:`).

    Here is the command syntax:

    ```dos
    net use [drive letter] \\[storage name].file.core.windows.net\[share name] /u:[storage account name] [storage account access key]
    ```

    Here's an example that assigns local drive letter `T:` to the Azure File Storage share:

    ```dos
    net use t: \\mystorage.file.core.windows.net\sharejson /u:myaccount hb5qy6eXLqIdBj0LvGMHdrTiygkjhHDvWjUZg3Gu7bubKLg==
    ```

    You can find the storage account key and the primary or secondary storage account access key in the Keys section of Settings in the Azure portal.

5.  Now you can access your JSON files from the Azure File Storage share by using the mapped drive, as shown in the following example:

    ```sql
    SELECT book.* FROM
     OPENROWSET(BULK N't:\books\books.json', SINGLE_CLOB) AS json
     CROSS APPLY OPENJSON(BulkColumn)
     WITH( id nvarchar(100), name nvarchar(100), price float,
        pages_i int, author nvarchar(100)) AS book
    ```

For more info about Azure File Storage, see [File storage](https://azure.microsoft.com/services/storage/files/).

## Import JSON documents from Azure Blob Storage

You can load files directly into Azure SQL Database from Azure Blob Storage with the T-SQL  BULK INSERT command or the OPENROWSET function.

First, create an external data source, as shown in the following example.

```sql
CREATE EXTERNAL DATA SOURCE MyAzureBlobStorage
 WITH ( TYPE = BLOB_STORAGE,
        LOCATION = 'https://myazureblobstorage.blob.core.windows.net',
        CREDENTIAL= MyAzureBlobStorageCredential);
```

Next, run a BULK INSERT command with the DATA_SOURCE option.

```sql
BULK INSERT Product
FROM 'data/product.dat'
WITH ( DATA_SOURCE = 'MyAzureBlobStorage');
```

## Parse JSON documents into rows and columns

Instead of reading an entire JSON file as a single value, you may want to parse it and return the books in the file and their properties in rows and columns. The following example uses a JSON file from [this site](https://github.com/tamingtext/book/blob/master/apache-solr/example/exampledocs/books.json) containing a list of books.

### Example 1

In the simplest example, you can just load the entire list from the file. 

```sql
SELECT value
 FROM OPENROWSET (BULK 'C:\JSON\Books\books.json', SINGLE_CLOB) as j
 CROSS APPLY OPENJSON(BulkColumn)
```

### Example 2

OPENROWSET reads a single text value from the file, returns it as a BulkColumn, and passes it to the OPENJSON function. OPENJSON iterates through the array of JSON objects in the BulkColumn array and returns one book in each row, formatted as JSON:

```json
{"id":"978-0641723445", "cat":["book","hardcover"], "name":"The Lightning Thief", ... }
{"id":"978-1423103349", "cat":["book","paperback"], "name":"The Sea of Monsters", ... }
{"id":"978-1857995879", "cat":["book","paperback"], "name":"Sophie's World : The Greek", ... } 
{"id":"978-1933988177", "cat":["book","paperback"], "name":"Lucene in Action, Second", ... }
```

### Example 3

The OPENJSON function can parse the JSON content and transform it into a table or a result set. The following example loads the content, parses the loaded JSON, and returns the five fields as columns:

```sql
SELECT book.*
 FROM OPENROWSET (BULK 'C:\JSON\Books\books.json', SINGLE_CLOB) as j
 CROSS APPLY OPENJSON(BulkColumn)
 WITH( id nvarchar(100), name nvarchar(100), price float,
 pages_i int, author nvarchar(100)) AS book
```

In this example, OPENROWSET(BULK) reads the content of the file and passes that content to the OPENJSON function with a defined schema for the output. OPENJSON matches properties in the JSON objects by using column names. For example, the `price` property is returned as a `price` column and converted to the float data type. Here are the results:

|Id|Name|price|pages_i|Author|
|---|---|---|---|---|
|978-0641723445|The Lightning Thief|12.5|384|Rick Riordan| 
|978-1423103349|The Sea of Monsters|6.49|304|Rick Riordan| 
|978-1857995879|Sophie's World : The Greek Philosophers|3.07|64|Jostein Gaarder| 
|978-1933988177|Lucene in Action, Second Edition|30.5|475|Michael McCandless|
||||||

Now you can return this table to the user, or load the data into another table.

## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [SQL Server 2016 and JSON Support](https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2016-and-JSON-Support)

-   [Using JSON in SQL Server 2016 and Azure SQL Database](https://channel9.msdn.com/Shows/Data-Exposed/Using-JSON-in-SQL-Server-2016-and-Azure-SQL-Database)

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven/SQLServer2016/JSON-as-a-bridge-betwen-NoSQL-and-relational-worlds)
  
## See Also

[Convert JSON Data to Rows and Columns with OPENJSON](../../relational-databases/json/convert-json-data-to-rows-and-columns-with-openjson-sql-server.md)

