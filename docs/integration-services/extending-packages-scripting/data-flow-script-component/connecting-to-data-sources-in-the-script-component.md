---
title: "Connecting to Data Sources in the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Script component [Integration Services], connecting to data sources"
ms.assetid: 96de63ab-ff48-4e7e-89e0-ffd6a89c63b6
author: janinezhang
ms.author: janinez
manager: craigg
---
# Connecting to Data Sources in the Script Component

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  A connection manager is a convenient unit that encapsulates and stores the information that is required to connect to a data source of a particular type. For more information, see [Integration Services &#40;SSIS&#41; Connections](../../../integration-services/connection-manager/integration-services-ssis-connections.md).  
  
 You can make existing connection managers available for access by the custom script in the source or destination component by clicking the **Add** and **Remove** buttons on the **Connection Managers** page of the **Script Transformation Editor**. However, you must write your own custom code to load or save your data, and possibly to open and close the connection to the data source. For more information about the **Connection Managers** page of the **Script Transformation Editor**, see [Configuring the Script Component in the Script Component Editor](../../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md) and [Script Transformation Editor &#40;Connection Managers Page&#41;](../../../integration-services/data-flow/transformations/script-transformation-editor-connection-managers-page.md).  
  
 The Script component creates a **Connections** collection class in the **ComponentWrapper** project item that contains a strongly-typed accessor for each connection manager that has the same name as the connection manager itself. This collection is exposed through the **Connections** property of the **ScriptMain** class. The accessor property returns a reference to the connection manager as an instance of <xref:Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSConnectionManager100>. For example, if you have added a connection manager named `MyADONETConnection` on the Connection Managers page of the dialog box, you can obtain a reference to it in your script by adding the following code:  
  
 `Dim myADONETConnectionManager As IDTSConnectionManager100 = _`  
  
 `Me.Connections.MyADONETConnection`  
  
> [!NOTE]  
>  You must know the type of connection that is returned by the connection manager before you call **AcquireConnection**. Because the Script task has **Option Strict** enabled, you must cast the connection, which is returned as type **Object**, to the appropriate connection type before you can use it.  
  
 Next, you call the **AcquireConnection** method of the specific connection manager to obtain either the underlying connection or the information that is required to connect to the data source. For example, you obtain a reference to the **System.Data.SqlConnection** wrapped by an ADO.NET connection manager by using the following code:  
  
 `Dim myADOConnection As SqlConnection = _`  
  
 `CType(MyADONETConnectionManager.AcquireConnection(Nothing), SqlConnection)`  
  
 In contrast, the same call to a flat file connection manager returns only the path and file name of the file data source.  
  
 `Dim myFlatFile As String = _`  
  
 `CType(MyFlatFileConnectionManager.AcquireConnection(Nothing), String)`  
  
 You then must provide this path and file name to a **System.IO.StreamReader** or **Streamwriter** to read or write the data in the flat file.  
  
> [!IMPORTANT]  
>  When you write managed code in a Script component, you cannot call the AcquireConnection method of connection managers that return unmanaged objects, such as the OLE DB connection manager and the Excel connection manager. However, you can read the ConnectionString property of these connection managers, and connect to the data source directly in your code by using the connection string of an OLEDB **connection** from the **System.Data.OleDb** namespace.  
>   
>  If you need to call the AcquireConnection method of a connection manager that returns an unmanaged object, use an ADO.NET connection manager. When you configure the ADO.NET connection manager to use an OLE DB provider, it connects by using the .NET Framework Data Provider for OLE DB. In this case, the AcquireConnection method returns a **System.Data.OleDb.OleDbConnection** instead of an unmanaged object. To configure an ADO.NET connection manager for use with an Excel data source, select the Microsoft OLE DB Provider for Jet, specify an Excel workbook, and then enter `Excel 8.0` (for Excel 97 and later) as the value of **Extended Properties** on the **All** page of the **Connection Manager** dialog box.  
  
 For more information about how to use connection managers with the script component, see [Creating a Source with the Script Component](../../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-source-with-the-script-component.md) and [Creating a Destination with the Script Component](../../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md).  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Connections](../../../integration-services/connection-manager/integration-services-ssis-connections.md)   
 [Create Connection Managers](https://msdn.microsoft.com/library/6ca317b8-0061-4d9d-b830-ee8c21268345)  
  
  
