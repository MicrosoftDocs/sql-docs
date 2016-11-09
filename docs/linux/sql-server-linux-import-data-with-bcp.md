---
# required metadata

title: Use bcp to bulk import data to SQL Server on Linux | SQL Server vNext CTP1
description: Describes how to use bcp to bulk import data into a SQL Server database on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/08/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 5c3b8e75-5f98-4355-b2f5-daf1f7d442ef

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""
---
# Use bcp to bulk import data to SQL Server on Linux

TODO: provide an example of using BCP on SQL Server on Linux. Point to the BCP documentation as well.

Optional: consider whether this discussion of BULK INSERT is applicable in this topic (from evaluation guide) and whether the same workaround for line ending is necessary with BCP.

## Bulk Insert

The Bulk Insert feature allows you to import records into a
table or view from a local data file.

To test this feature, create a new directory /var/opt/mssql/Samples and a new file SampleLocations.txt with the following content:

    1,Subassembly
    2,Storage
    3,Assembly
    4,Final Assembly
    5,Paint Shop

Linux and Windows handle newlines differently and SQL Server on Linux only recognizes newline characters in the Windows format this preview release.  If you authored the SampleLocations.txt file in Linux, download the dos2unix package and use it to create a version of the file that will be interpreted correctly by SQL Server.  

    $ apt-get install dos2unix
    
    $ unix2dos –n SampleLocations.txt WinSampleLocations.txt 

Create a new table in tempdb for Location data:

    use tempdb

    CREATE TABLE Locations ( LocationID int, Location varchar(255) )

Use Bulk Insert to populate the Locations table with data from your text file:

    BULK INSERT Locations FROM 'C:\Samples\WinSampleLocations.txt' WITH (FIELDTERMINATOR = ',')
