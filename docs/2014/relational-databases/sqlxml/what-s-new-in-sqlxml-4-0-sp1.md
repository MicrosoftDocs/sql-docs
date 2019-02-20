---
title: "What&#39;s New in SQLXML 4.0 SP1 | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "registry keys [SQLXML]"
  - "SQLNCLI, SQLXML"
  - "what's new [SQLXML]"
  - "SQLXML, what's new"
  - "migrating SQLXML applications"
  - "redistributing SQLXML"
  - "SQL Server Native Client, SQLXML"
  - "side-by-side installations [SQLXML]"
ms.assetid: 48f7720b-1705-402d-93ce-097ff1737877
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# What&#39;s New in SQLXML 4.0 SP1
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQLXML 4.0 SP1 includes various updates and enhancements. This topic summarizes the updates and provides links to more detailed information, where available. SQLXML 4.0 SP1 provides additional enhancements to support the new data types introduced in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]. This topic includes the following subjects:  
  
-   Installing SQLXML 4.0 SP1  
  
-   Side-by-Side Installation Issues  
  
-   SQLXML 4.0 and MSXML  
  
-   Redistributing SQLXML 4.0  
  
-   Support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client  
  
-   Support for Data Types Introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]  
  
-   XML Bulk Load Changes for SQLXML 4.0  
  
-   Registry Key Changes for SQLXML 4.0  
  
-   Migration Issues  
  
## Installing SQLXML 4.0 SP1  
 Before [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], SQLXML 4.0 was released with SQL Server and was part of the default installation of all SQL Server versions except for SQL Server Express. Starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], the latest version of SQLXML (SQLXML 4.0 SP1) is no longer included in SQL Server. To install SQLXML 4.0 SP1, download it from [Install Location for SQLXML 4.0 SP1](https://www.microsoft.com/download/details.aspx?id=30403).  
  
 The SQLXML 4.0 SP1 files are installed in the following location:  
  
 `%PROGRAMFILES%\SQLXML 4.0\`  
  
> [!NOTE]  
>  All appropriate registry settings for SQLXML 4.0 are made as part of the installation process.  
  
 To allow 32-bit SQLXML applications to run under Windows on Windows (WOW64) on 64-bit Windows operating systems, run the 64-bit SQLXML 4.0 SP1 package, named sqlxml4.msi, which can be found on the Download Center.  
  
#### Uninstalling SQLXML 4.0 SP1  
 Shared registry keys exist between SQLXML 3.0 SP3, SQLXML 4.0 and SQLXML 4.0 SP1. If the later versions of SQLXML are uninstalled on the same computer which contains SQLXML 3.0 SP3, you might need to reinstall SQLXML 3.0 SP3.  
  
## Side-by-Side Installation Issues  
 The installation process for SQLXML 4.0 does not remove the files that were installed by earlier versions of SQLXML. Therefore, you can have DLLs for several different version-distinctive installations of SQLXML on your computer. You can run the installations side-by-side. SQLXML 4.0 includes both version-independent and version-dependent PROGIDs. All production applications should use version-dependent PROGIDs.  
  
## SQLXML 4.0 SP1 and MSXML  
 SQLXML 4.0 does not install MSXML. SQLXML 4.0 uses MSXML 6.0, which is installed as part of the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later installation.  
  
## Redistributing SQLXML 4.0 SP1  
 You can distribute SQLXML 4.0 SP1 using the redistributable installer package. One way to install multiple packages in what seems to the user to be a single installation is to use chainer and bootstrapper technology. For more information, see Authoring a Custom Bootstrapper Package for Visual Studio 2005 and Adding Custom Prerequisites.  
  
 If your application targets a platform other than the one it was developed on, you can download versions of sqlncli.msi for x64, Itanium, and x86 from the Microsoft Download Center.  
  
 There are also separate redistribution installation programs for MSXML 6.0 (msxml6.msi). These can be found on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation CD in the following location:  
  
 `%CD%\Setup\`  
  
 These installation files can be used to install MSXML 6.0 directly from the CD. They can also be used to freely redistribute MSXML 6.0 along with SQLXML 4.0 SP1 with your own custom applications.  
  
 You will also need to redistribute [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client if you are using it as the data provider with your application. For more information, see [Installing SQL Server Native Client](../native-client/applications/installing-sql-server-native-client.md).  
  
## Support for SQL Server Native Client  
 SQLXML 4.0 supports both the SQLOLEDB and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client providers. It is recommended that you use the same version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client provider and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is developed to support any new data types that ship in the server, such as the `Date, Time`, `DateTime2`, and `dateTimeOffset` data types in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and supported by [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Native Client.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is a data access technology that was introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. It combines the SQLOLEDB Provider and the SQLODBC Driver into one native dynamic link library (DLL), while also providing new functionality that is separate and distinct from the Microsoft Data Access Components (MDAC).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client can be used to create new applications or enhance existing applications that need to take advantage of features introduced in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are not supported by SQLOLEDB and SQLODBC in MDAC and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows. For example, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is required for client-side SQLXML features, such as FOR XML, to use the `xml` data type. For more information, see [Client-side XML Formatting &#40;SQLXML 4.0&#41;](formatting/client-side-xml-formatting-sqlxml-4-0.md), [Using ADO to Execute SQLXML 4.0 Queries](using-ado-to-execute-sqlxml-4-0-queries.md), and [SQL Server Native Client Programming](../native-client/sql-server-native-client-programming.md).  
  
> [!NOTE]  
>  SQLXML 4.0 is not completely backward compatible with SQLXML 3.0. Because of some bug fixes and other functional changes, particularly the removal of SQLXML ISAPI support, you cannot use IIS virtual directories with SQLXML 4.0. Although most applications will run with minor modifications, you must test them before putting them into production with SQLXML 4.0.  
  
## Support for Data Types Introduced in SQL Server 2005 and SQL Server 2008  
 [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] introduced the `xml` data type, and SQLXML 4.0 supports the `xml` data type. For more information, see [xml Data Type Support in SQLXML 4.0](xml-data-type-support-in-sqlxml-4-0.md).  
  
 For examples of how to use the `xml` data type in SQLXML when mapping XML views, bulk loading XML or executing XML updategrams, refer to examples provided in the following topics.  
  
-   [Default Mapping of XSD Elements and Attributes to Tables and Columns](../sqlxml-annotated-xsd-schemas-using/default-mapping-of-xsd-elements-and-attributes-to-tables-and-columns-sqlxml-4-0.md)  
  
-   [Inserting Data by Using XML Updategrams](../sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/inserting-data-using-xml-updategrams-sqlxml-4-0.md)  
  
-   [Examples of Bulk Loading XML Documents](../sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/xml-bulk-load-examples-sqlxml-4-0.md)  
  
 [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] introduced the `Date, Time`, `DateTime2`, and **DateTimeOffset** data types. SQLXML 4.0 SP1 will enable these four new data types as built-in scalar types when used with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Native Client OLE DB Provider (SQLNCLI11), which ships in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## XML Bulk Load Changes for SQLXML 4.0 SP1  
  
-   For SQLXML 4.0, the SchemaGen overflow field is created using the `xml` data type. For more information, see [SQL Server XML Bulk Load Object Model](../sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/sql-server-xml-bulk-load-object-model-sqlxml-4-0.md).  
  
-   If you have previously created [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic applications and you want to use SQLXML 4.0, you must recompile the application with reference to Xblkld4.dll.  
  
-   For Visual Basic Scripting Edition applications, you must register the DLL you want to use. In the following example, if you specify version-independent PROGIDs, the application depends on the last registered DLL:  
  
    ```  
    set objBulkLoad = CreateObject("SQLXMLBulkLoad.SQLXMLBulkLoad")   
    ```  
  
    > [!NOTE]  
    >  The version-dependent PROGID is SQLXMLBulkLoad.SQLXMLBulkLoad.4.0.  
  
## Registry Key Changes for SQLXML 4.0  
 In SQLXML 4.0, the registry keys have changed from the earlier releases to the following:  
  
 HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SQLXML4\TemplateCacheSize  
  
 HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SQLXML4\SchemaCacheSize  
  
 You must change the settings if you want these keys to be in effect for SQLXML 4.0.  
  
 In addition, SQLXML 4.0 introduces the following registry keys:  
  
-   `HKEY_LOCAL_MACHINE\Software\Microsoft\MSSQLServer\Client\SQLXML4\ReportErrorsWithSQLInfo`  
  
     By default, SQLXML 4.0 returns native error information provided by OLE DB and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instead of a high-level SQLXML error (as was the case in earlier versions of SQLXML). If you do not want this behavior, the value of this registry key of type DWORD must be set to 0 (default is 1).  
  
-   HKEY_LOCAL_MACHINE\Software\Microsoft\MSSQLServer\Client\SQLXML4\FORXML_GenerateGUIDBraces  
  
     By default, SQLXML return SQL Server GUID values without the enclosing braces. If you want the GUID value returned with the braces (for example, {*some GUID*}), value of this registry key must be set to 1 (default is 0).  
  
-   HKEY_LOCAL_MACHINE\Software\Microsoft\MSSQLServer\Client\SQLXML4\SQL2000CompatMode  
  
     By default, when the XML parser loads the data, white spaces are normalized according to the XML 1.0 rules. This results in loss of some of the white space characters in your data. Thus, the textual representation of your data may not be the same after parsing, although semantically the data is the same.  
  
     This key is introduced so that you can choose to keep the white space characters in the data. If you add this registry key and set its value to 0, white space characters (LF, CR, and tab) in the XML are returned encoded in case of attribute values. In case of element values, only CR is returned encoded.  
  
     For example:  
  
    ```  
    CREATE TABLE T( Col1 int, Col2 nvarchar(100));  
    GO  
    -- Insert data with tab, line feed and carriage return).  
    INSERT INTO T VALUES (1, 'This is a tab    . This is a line feed and CR   
     more text');  
    GO  
    -- Test this query (without the registry key).  
    SELECT * FROM T   
    FOR XML AUTO;  
    -- This is the result (no encoding of special characters).  
    <?xml version="1.0" encoding="utf-8" ?>  
    <r>  
      <T Col1="1"   
         Col2="This is a tab    . This is a line feed and CR   
     more text"/>  
    </r>  
    -- Now add registry key with value 0 and execute the query again.  
    -- Note the encoding for carriage return, line-feed and tab in the attribute value.  
    <?xml version="1.0" encoding="utf-8" ?>  
    <r>  
      <T Col1="1"   
         Col2="This is a tab    . This is a line feed and CR   
     more text"/>  
    </r>  
  
    -- Update the query and specify ELEMENTS directive  
    SELECT * FROM T  
    FOR XML AUTO, ELEMENTS  
    -- Only the carriage return is returned encoded.  
    <?xml version="1.0" encoding="utf-8" ?>  
    <r>  
       <T>  
          <Col1>1</Col1>  
          <Col2>This is a tab    . This is a line feed and CR   
     more text</Col2>  
       </T>  
    </r>  
    ```  
  
## Migration Issues  
 The following are issues that could impact migration of your legacy SQLXML applications to SQLXML 4.0.  
  
### ADO and SQLXML 4.0 Queries  
 In earlier versions of SQLXML, support for URL-based query execution using IIS virtual directories and the SQLXML ISAPI filter was provided. For applications that use SQLXML 4.0, this support is no longer available.  
  
 Instead, SQLXML queries, templates, and updategrams can be executed by using the SQLXML extensions to ActiveX Data Objects (ADO) first introduced in Microsoft Data Access Components (MDAC) 2.6 and later.  
  
 For more information, see [Using ADO to Execute SQLXML 4.0 Queries](using-ado-to-execute-sqlxml-4-0-queries.md).  
  
### Supportability for SQLXML 3.0 ISAPI and Data Types Introduced in SQL Server 2005  
 Because ISAPI support has been removed from SQLXML 4.0, if your solution requires the enhanced data typing features introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] such as the [xml data type](/sql/t-sql/xml/xml-transact-sql) or [user-defined data types (UDTs)](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) and Web-based access, you will need to use another solution such as [SQLXML managed classes](../sqlxml-annotated-xsd-schemas-xpath-queries/net-framework-classes/sqlxml-4-0-net-framework-support-managed-classes.md) or another type of HTTP handler, such as Native XML Web Services for SQL Server 2005.  
  
 Alternately, if you do not require these type extensions, you can continue to use SQLXML 3.0 to connect to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] installations. The SQLXML 3.0 ISAPI support will work against these later versions but does not support or recognize the `xml` data type or UDT type support introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
### XML Bulk Load Security Changes for Temporary Files  
 For SQLXML 4.0 and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], XML Bulk Load file permissions are granted to the user executing the bulk load operation. Read and Write permissions are inherited from the file system. In previous releases of SQLXML and SQL Server, XML Bulk Load under SQLXML would create temporary files that were not secured and could be readable by anyone.  
  
### Migration Issues for Client-Side FOR XML  
 Due to changes in the execution engine, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might return different values in the metadata for a base table than would be returned if the FOR XML query was executed under [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]. In cases where this occurs, client-side formatting of the FOR XML query results will have differing output depending on which version the query is run against.  
  
 If a FOR XML query is executed client-side using SQLXML 3.0 over an `xml` data type column, the data in the results will come back as a fully entitized string. In SQLXML 4.0, if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client (SQLNCLI11) is specified as the provider, the data will be returned as XML.  
  
  
