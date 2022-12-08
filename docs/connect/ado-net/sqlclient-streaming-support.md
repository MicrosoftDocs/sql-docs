---
title: "SqlClient streaming support"
description: Discusses how to write applications that stream data from SQL Server without having it fully loaded in memory.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "12/04/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# SqlClient streaming support

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Streaming support between SQL Server and an application supports unstructured data on the server (documents, images, and media files). A SQL Server database can store binary large objects (BLOBs), but retrieving BLOBS can use a lot of memory.

Streaming support to and from SQL Server simplifies writing applications that stream data, without having to fully load the data into memory, resulting in fewer memory overflow exceptions.

Streaming support will also enable middle-tier applications to scale better, especially in scenarios where business objects connect to Azure SQL in order to send, retrieve, and manipulate large BLOBs.

> [!WARNING]
> The members that support streaming are used to retrieve data from queries and to pass parameters to queries and stored procedures. The streaming feature addresses basic OLTP and data migration scenarios and is applicable to on-premises and off-premises data migrations environments.

## Streaming support from SQL Server

Streaming support from SQL Server introduces new functionality in the <xref:System.Data.Common.DbDataReader> and in the <xref:Microsoft.Data.SqlClient.SqlDataReader> classes in order to get <xref:System.IO.Stream>, <xref:System.Xml.XmlReader>, and <xref:System.IO.TextReader> objects and react to them. These classes are used to retrieve data from queries. As a result, Streaming support from SQL Server addresses OLTP scenarios and applies to on-premises and off-premises environments.

The following members were added to <xref:Microsoft.Data.SqlClient.SqlDataReader> to enable streaming support from SQL Server:

- <xref:Microsoft.Data.SqlClient.SqlDataReader.IsDBNullAsync%2A>

- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetFieldValue%2A?displayProperty=nameWithType>

- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetFieldValueAsync%2A>

- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetStream%2A>

- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetTextReader%2A>

- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetXmlReader%2A>

The following members were added to <xref:System.Data.Common.DbDataReader> to enable streaming support from SQL Server:

- <xref:System.Data.Common.DbDataReader.GetFieldValue%2A>

- <xref:System.Data.Common.DbDataReader.GetStream%2A>

- <xref:System.Data.Common.DbDataReader.GetTextReader%2A>

## Streaming support to SQL Server

Streaming support to SQL Server is in the <xref:Microsoft.Data.SqlClient.SqlParameter> class so it can accept and react to <xref:System.Xml.XmlReader>, <xref:System.IO.Stream>, and <xref:System.IO.TextReader> objects. <xref:Microsoft.Data.SqlClient.SqlParameter> is used to pass parameters to queries and stored procedures.

> [!NOTE]
> Disposing a <xref:Microsoft.Data.SqlClient.SqlCommand> object or calling <xref:Microsoft.Data.SqlClient.SqlCommand.Cancel%2A> must cancel any streaming operation. If an application sends <xref:System.Threading.CancellationToken>, cancellation is not guaranteed.

The following <xref:Microsoft.Data.SqlClient.SqlParameter.SqlDbType%2A> types will accept a <xref:Microsoft.Data.SqlClient.SqlParameter.Value%2A> of <xref:System.IO.Stream>:

- **Binary**

- **VarBinary**

The following <xref:Microsoft.Data.SqlClient.SqlParameter.SqlDbType%2A> types will accept a <xref:Microsoft.Data.SqlClient.SqlParameter.Value%2A> of <xref:System.IO.TextReader>:

- **Char**

- **NChar**

- **NVarChar**

- **Xml**

The **Xml**<xref:Microsoft.Data.SqlClient.SqlParameter.SqlDbType%2A> type will accept a <xref:Microsoft.Data.SqlClient.SqlParameter.Value%2A> of <xref:System.Xml.XmlReader>.

<xref:Microsoft.Data.SqlClient.SqlParameter.SqlValue%2A> can accept values of type <xref:System.Xml.XmlReader>, <xref:System.IO.TextReader>, and <xref:System.IO.Stream>.

The <xref:System.Xml.XmlReader>, <xref:System.IO.TextReader>, and <xref:System.IO.Stream> object will be transferred up to the value defined by the <xref:Microsoft.Data.SqlClient.SqlParameter.Size%2A>.

## Sample -- streaming from SQL Server

Use the following Transact-SQL to create the sample database:

```sql
CREATE DATABASE [Demo]
GO
USE [Demo]
GO
CREATE TABLE [Streams] (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[textdata] NVARCHAR(MAX),
[bindata] VARBINARY(MAX),
[xmldata] XML)
GO
INSERT INTO [Streams] (textdata, bindata, xmldata) VALUES (N'This is a test', 0x48656C6C6F, N'<test>value</test>')
INSERT INTO [Streams] (textdata, bindata, xmldata) VALUES (N'Hello, World!', 0x54657374696E67, N'<test>value2</test>')
INSERT INTO [Streams] (textdata, bindata, xmldata) VALUES (N'Another row', 0x666F6F626172, N'<fff>bbb</fff><fff>bbc</fff>')
GO
```

The sample shows how to do the following:

- Avoid blocking a user-interface thread by providing an asynchronous way to retrieve large files.

- Transfer a large text file from SQL Server in .NET.

- Transfer a large XML file from SQL Server in .NET.

- Retrieve data from SQL Server.

- Transfer large files (BLOBs) from one SQL Server database to another without running out of memory.

[!code-csharp[SqlClient_Streaming_FromServer#1](~/../sqlclient/doc/samples/SqlClient_Streaming_FromServer.cs#1)]

## Sample -- streaming to SQL Server

Use the following Transact-SQL to create the sample database:

```sql
CREATE DATABASE [Demo2]
GO
USE [Demo2]
GO
CREATE TABLE [BinaryStreams] (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[bindata] VARBINARY(MAX))
GO
CREATE TABLE [TextStreams] (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[textdata] NVARCHAR(MAX))
GO
CREATE TABLE [BinaryStreamsCopy] (
[id] INT PRIMARY KEY IDENTITY(1, 1),
[bindata] VARBINARY(MAX))
GO
```

The sample shows how to do the following:

- Transferring a large BLOB to SQL Server in .NET.

- Transferring a large text file to SQL Server in .NET.

- Using the new asynchronous feature to transfer a large BLOB.

- Using the new asynchronous feature and the await keyword to transfer a large BLOB.

- Cancelling the transfer of a large BLOB.

- Streaming from one SQL Server to another using the asynchronous feature.

[!code-csharp[SqlClient_Streaming_ToServer#1](~/../sqlclient/doc/samples/SqlClient_Streaming_ToServer.cs#1)]

## Sample -- Streaming from one SQL Server to another SQL Server

This sample demonstrates how to asynchronously stream a large BLOB from one SQL Server to another, with support for cancellation.

> [!NOTE]
> Before running the following sample, be sure the Demo and Demo2 databases are created.

[!code-csharp[SqlClient_Streaming_ServerToServer#1](~/../sqlclient/doc/samples/SqlClient_Streaming_ServerToServer.cs#1)]

## See also

- [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
