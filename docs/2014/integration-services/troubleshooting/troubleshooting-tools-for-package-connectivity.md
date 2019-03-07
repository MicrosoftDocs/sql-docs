---
title: "Troubleshooting Tools Package Connectivity | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Integration Services packages, troubleshooting"
  - "SSIS packages, troubleshooting"
  - "Integration Services, troubleshooting"
  - "connectivity [Integration Services], troubleshooting"
  - "errors [Integration Services], troubleshooting"
  - "packages [Integration Services], troubleshooting"
ms.assetid: 08a019f5-8ba7-4527-97c1-e9846d4022ff
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Troubleshooting Tools Package Connectivity
  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes features and tools that you can use to troubleshoot connectivity between packages and the data sources from which packages extract and load data.  
  
## Troubleshooting Issues with External Data Providers  
 Many packages fail during interactions with external data providers. However, the messages that those providers return to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] frequently do not provide enough information to start troubleshooting the interaction. To address this troubleshooting need, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes logging messages that you can use to troubleshoot a package's interaction with external data sources.  
  
-   **Enable logging and select the package's Diagnostic event to see the troubleshooting messages**. The following [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components are capable of writing a message to the log before and after every call to an external data provider:  
  
    -   OLE DB connection manager, OLE DB source, and OLE DB destination  
  
    -   [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager and ADO NET source  
  
    -   Execute SQL task  
  
    -   Lookup transformation, OLE DB Command transformation, and Slowly Changing Dimension transformation  
  
     The log messages include the name of the method being called. For example, these log messages might include the `Open` method of an OLE DB `Connection` object or the `ExecuteNonQuery` method of a `Command` object. The messages have the following format, where '%1!s!' is a placeholder for the method information:  
  
    ```  
    ExternalRequest_pre: The object is ready to make the following external request: '%1!s!'.  
    ExternalRequest_post: '%1!s!'. The external request has completed.  
    ```  
  
     To troubleshoot the interaction with the external data provider, review the log to see whether every "before" message (`ExternalRequest_pre`) has a corresponding "after" message (`ExternalRequest_post`). If there is no corresponding "after" message, you know that the external data provider did not respond as expected.  
  
     The following example shows some sample rows from a log that contains these logging messages:  
  
    ```  
    ExternalRequest_pre: The object is ready to make the following external request: 'ITransactionJoin::JoinTransaction'.  
    ExternalRequest_post: 'ITransactionJoin::JoinTransaction succeeded'. The external request has completed.  
    ExternalRequest_pre: The object is ready to make the following external request: 'IDbConnection.Open'.  
    ExternalRequest_post: 'IDbConnection.Open succeeded'. The external request has completed.  
    ExternalRequest_pre: The object is ready to make the following external request: 'IDbConnection.CreateCommand'.  
    ExternalRequest_post: 'IDbConnection.CreateCommand finished'. The external request has completed."  
    ExternalRequest_pre: The object is ready to make the following external request: 'IDbCommand.ExecuteReader'.  
    ExternalRequest_post: 'IDbCommand.ExecuteReader finished'. The external request has completed."  
    ExternalRequest_pre: The object is ready to make the following external request: 'IDataReader.GetSchemaTable'.  
    ExternalRequest_post: 'IDataReader.GetSchemaTable finished'. The external request has completed."  
    ExternalRequest_pre: The object is ready to make the following external request: 'IDataReader.Close'.  
    ExternalRequest_post: 'IDataReader.Close finished'. The external request has completed."  
    ExternalRequest_pre: The object is ready to make the following external request: 'IDbConnection.Close'.  
    ExternalRequest_post: 'IDbConnection.Close finished'. The external request has completed."  
    ```  
  
## See Also  
 [Troubleshooting Tools for Package Development](troubleshooting-tools-for-package-development.md)   
 [Troubleshooting Tools for Package Execution](troubleshooting-tools-for-package-execution.md)  
  
  
