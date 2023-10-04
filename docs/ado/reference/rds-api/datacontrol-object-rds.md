---
title: "DataControl Object (RDS)"
description: "DataControl Object (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "DataControl"
  - "RDS.DataControl"
helpviewer_keywords:
  - "DataControl object [ADO]"
apitype: "COM"
---
# DataControl Object (RDS)
Binds a data query [Recordset](../ado-api/recordset-object-ado.md) to one or more controls (for example, a text box, grid control, or combo box) to display the **Recordset** data on a Web page.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
<OBJECT CLASSID="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33" ID="DataControl"  
   <PARAM NAME="Connect" VALUE="DSN=DSNName;UID=MyUserID;PWD=MyPassword;">  
   <PARAM NAME="Server" VALUE="https://awebsrvr">  
   <PARAM NAME="SQL" VALUE="QueryText">  
</OBJECT>  
```  
  
## Remarks  
 The class ID for the **RDS.DataControl** object is BD96C556-65A3-11D0-983A-00C04FC29E33.  
  
> [!NOTE]
>  If you get an error that an [RDS.DataSpace](./dataspace-object-rds.md) or **RDS.DataControl** object does not load, make sure that you are using the correct class ID. The class IDs for these objects have changed from version 1.0 and 1.1. Also, be aware that even nullable columns must be set when you use the **RDS DataControl** object.  
  
 For a basic scenario, you need to set only the **SQL**, **Connect**, and **Server** properties of the **RDS.DataControl** object, which will automatically call the default business object, [RDSServer.DataFactory](./datafactory-object-rdsserver.md).  
  
 All the properties in the **RDS.DataControl** are optional because custom business objects can replace their functionality.  
  
> [!NOTE]
>  If you query for multiple results, only the first [Recordset](../ado-api/recordset-object-ado.md) is returned. If multiple result sets are needed, assign each to its own **DataControl**. An example of a query for multiple results could be the following: `"Select * from Authors, Select * from Topics"`  
  
 Adding "DFMode=20;" to your connection string when you use the **RDS.DataControl** object can improve the performance of your server when you update data. With this setting, the **RDSServer.DataFactory** object on the server uses a less resource-intensive mode. However, the following features are not available in this configuration:  
  
-   Using parameterized queries.  
  
-   Getting parameter or column information before calling the **Execute** method.  
  
-   Setting **Transact Updates** to **True**.  
  
-   Getting row status.  
  
-   Calling the [Resync](../ado-api/resync-method.md) method.  
  
-   Refreshing (explicitly or automatically) via the [Update Resync](../ado-api/update-resync-property-dynamic-ado.md) property.  
  
-   Setting **Command** or [Recordset](./recordset-sourcerecordset-properties-rds.md) properties.  
  
-   Using **adCmdTableDirect**.  
  
 The **RDS.DataControl** object runs in asynchronous mode by default. If you require synchronous execution for your application, set the [ExecuteOptions](./executeoptions-property-rds.md) parameter equal to **adcExecSync** and the [FetchOptions](./fetchoptions-property-rds.md) parameter equal to **adcFetchUpFront**, as shown in the following example.  
  
```  
<OBJECT CLASSID="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33"   
    ID="DataControl"  
   <PARAM NAME="Connect" VALUE="DSN=DSNName;UID=MyUserID;PWD=MyPassword;">  
   <PARAM NAME="Server" VALUE="https://awebsrvr">  
   <PARAM NAME="SQL" VALUE="QueryText">  
   <PARAM NAME="ExecuteOptions" VALUE="1">   <PARAM NAME="FetchOptions" VALUE="1">  
</OBJECT>  
```  
  
 Use one **RDS.DataControl** object to link the results of a single query to one or more visual controls. For example, suppose you code a query requesting customer data such as Name, Residence, Place of Birth, Age, and Priority Customer Status. You can use a single **RDS.DataControl** object to display a customer's Name, Age, and Region in three separate text boxes; Priority Customer Status in a check box; and all the data in a grid control.  
  
 Use different **RDS.DataControl** objects to link the results of multiple queries to different visual controls. For example, suppose you use one query to obtain information about a customer, and a second query to obtain information about merchandise that the customer has purchased. You want to display the results of the first query in three text boxes and one check box, and the results of the second query in a grid control. If you use the default business object (**RDSServer.DataFactory**), you must do the following:  
  
-   Add two **RDS.DataControl** objects to your Web page.  
  
-   Write two queries, one for each **SQL** property of the two **RDS.DataControl** objects. One **RDS.DataControl** object will contain an SQL query requesting customer information; the second will contain a query requesting a list of merchandise the customer has purchased.  
  
-   In the OBJECT tags of each bound control, specify the DATAFLD value to set the values for the data that you want to display in each visual control.  
  
 There is no count restriction on the number of **RDS.DataControl** objects that you can embed by using OBJECT tags on a single Web page.  
  
 When you define the **RDS.DataControl** object on a Web page, use nonzero **Height** and **Width** values such as 1 (to avoid the inclusion of extra space).  
  
 Remote Data Service client components are already included as part of Internet Explorer 4.0; therefore, you do not need to include a CODEBASE parameter in your **RDS.DataControl** object tag.  
  
 With Internet Explorer 4.0 or later, you can bind to data by using HTML controls and ActiveX® controls only if they are marked as apartment model controls.  
  
> [!NOTE]
>  **Microsoft Visual Basic Users** The **RDS.DataControl** is safe for scripting and is used only in Web-based applications. A Visual Basic client application has no need for it.  
  
 This section contains the following topic.  
  
-   [DataControl Object (RDS) Properties, Methods, and Events](./datacontrol-object-rds-properties-methods-and-events.md)  
  
## See Also  
 [DataControl Object Example (VBScript)](./datacontrol-object-example-vbscript.md)