---
title: "Absolute and Relative URLs"
description: "Absolute and Relative URLs"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "relative URLs [ADO]"
  - "absolute URLs [ADO]"
  - "URLs [ADO]"
---
# Absolute and Relative URLs
A URL specifies the location of a target stored on a local or networked computer. The target can be a file, directory, HTML page, image, program, and so on.  
  
 An *absolute URL* contains all the information necessary to locate a resource.  
  
 A *relative URL* locates a resource using an absolute URL as a starting point. In effect, the "complete URL" of the target is specified by concatenating the absolute and relative URLs.  
  
 An *absolute URL* uses the following format: *scheme://server/path/resource*  
  
 A relative URL typically consists only of the *path*, and optionally, the *resource*, but no *scheme* or *server*. The following tables define the individual parts of the complete URL format.  
  
 *scheme*  
 Specifies how the *resource* is to be accessed.  
  
 *server*  
 Specifies the name of the computer where the *resource* is located.  
  
 *path*  
 Specifies the sequence of directories leading to the target. If *resource* is omitted, the target is the last directory in *path*.  
  
 *resource*  
 If included, *resource* is the target, and is typically the name of a file. It may be a *simple file,* containing a single binary stream of bytes, or a *structured document,* containing one or more storages and binary streams of bytes.  
  
## URL Scheme Registration  
 If a provider supports URLs, the provider will register one or more URL schemes. Registration means that any URLs using the scheme will automatically invoke the registered provider. For example, the *http* scheme is registered to the [Microsoft OLE DB Provider for Internet Publishing](../appendixes/microsoft-ole-db-provider-for-internet-publishing.md). ADO assumes all URLs prefixed with "http" represent Web folders or files to be used with the Internet Publishing Provider. For information about the schemes registered by your provider, see your provider documentation.  
  
## Defining Context with a URL  
 One function of an open connection, represented by a [Connection](../../reference/ado-api/connection-object-ado.md) object, is to restrict subsequent operations to the data source represented by that connection. That is, the connection defines the context for subsequent operations.  
  
 With ADO 2.7 or later, an absolute URL can also define a context. For example, when a [Record](../../reference/ado-api/record-object-ado.md) object is opened with an absolute URL, a **Connection** object is implicitly created to represent the resource specified by the URL.  
  
 An absolute URL that defines a context can be specified in the *ActiveConnection* parameter of the **Record** object [Open](../../reference/ado-api/open-method-ado-record.md) method. An absolute URL can also be specified as the value of the "URL=" keyword in the **Connection** object [Open](../../reference/ado-api/open-method-ado-connection.md) method *ConnectionString* parameter, and the [Recordset](../../reference/ado-api/recordset-object-ado.md) object [Open](../../reference/ado-api/open-method-ado-recordset.md) method *ActiveConnection* parameter.  
  
 Context can also be defined by opening a **Record** or **Recordset** object that represents a directory, because these objects already have an implicitly or explicitly declared **Connection** object that specifies context.  
  
## Scoped Operations  
 The context also defines scope-that is, the directory and its subdirectories that can participate in subsequent operations. The **Record** object has several scoped methods that operate on a directory and all its subdirectories. These methods include [CopyRecord](../../reference/ado-api/copyrecord-method-ado.md), [MoveRecord](../../reference/ado-api/moverecord-method-ado.md), and [DeleteRecord](../../reference/ado-api/deleterecord-method-ado.md).  
  
## Relative URLs as Command Text  
 You can specify a command to be executed on the data source by typing a string in the *CommandText* parameter of the **Connection** object's [Execute](../../reference/ado-api/execute-method-ado-connection.md) method, and in the *Source* parameter of the **Recordset** object's [Open](../../reference/ado-api/open-method-ado-recordset.md) method.  
  
 A relative URL can be specified in the *CommandText* or *Source* parameter. The relative URL does not actually represent a command, such as an SQL command; it merely specifies the parameters. The context of the active connection must be an absolute URL, and the *Option* parameter must be set to **adCmdTableDirect**.  
  
 For example, the following code sample shows how to open a **Recordset** on the Readme25.txt file of the Winnt/system32 directory:  
  
```  
recordset.Open "system32/Readme25.txt", "URL=https://YourServer/Winnt/",,,adCmdTableDirect  
```  
  
 The absolute URL in the connection string specifies the server (`YourServer`) and the path (`Winnt`). This URL also defines the context.  
  
 The relative URL in the command text uses the absolute URL as a starting point and specifies the remainder of the path (`system32`) and the file to open (`Readme25.txt`).  
  
 The options field (`adCmdTableDirect`) indicates that the command type is a relative URL.  
  
 As another example, the following code will open a **Recordset** on the contents of the `Winnt` directory:  
  
```  
recordset.Open "", "URL=https://YourServer/Winnt/",,,adCmdTableDirect  
```  
  
## OLE DB Provider-Supplied URL Schemes  
 The leading part of a fully-qualified URL is the *scheme* that is used to access the resource identified by the remainder of the URL. Examples are HTTP (Hypertext Transfer Protocol) and FTP (File Transfer Protocol).  
  
 ADO supports OLE DB providers that recognize their own URL schemes. For example, the [Microsoft OLE DB Provider for Internet Publishing](../appendixes/microsoft-ole-db-provider-for-internet-publishing.md)*,* which accesses "published" Windows 2000 files, recognizes the existing HTTP scheme.  
  
## See Also  
 [Connection Object (ADO)](../../reference/ado-api/connection-object-ado.md)   
 [Record Object (ADO)](../../reference/ado-api/record-object-ado.md)   
 [Recordset Object (ADO)](../../reference/ado-api/recordset-object-ado.md)