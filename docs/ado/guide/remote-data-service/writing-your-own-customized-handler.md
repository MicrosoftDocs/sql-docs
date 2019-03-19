---
title: "Writing Your Own Customized Handler | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "DataFactory handler in RDS [ADO]"
  - "customized handler in RDS [ADO]"
ms.assetid: d447712a-e123-47b5-a3a4-5d366cfe8d72
author: MightyPen
ms.author: genemi
manager: craigg
---
# Writing Your Own Customized Handler
You may want to write your own handler if you are an IIS server administrator who wants the default RDS support, but more control over user requests and access rights.  
  
 The MSDFMAP.Handler implements the **IDataFactoryHandler** interface.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## IDataFactoryHandler Interface  
 This interface has two methods, **GetRecordset** and **Reconnect**. Both methods require that the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property be set to **adUseClient**.  
  
 Both methods take arguments that appear after the first comma in the "**Handler=**" keyword. For example, `"Handler=progid,arg1,arg2;"` will pass an argument string of `"arg1,arg2"`, and `"Handler=progid"` will pass a null argument.  
  
## GetRecordset Method  
 This method queries the data source and creates a new [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object using the arguments provided. The **Recordset** must be opened with **adLockBatchOptimistic** and must not be opened asynchronously.  
  
### Arguments  
 ***conn***  The connection string.  
  
 ***args***  The arguments for the handler.  
  
 ***query***  The command text for making a query.  
  
 ***ppRS***  The pointer where the **Recordset** should be returned.  
  
## Reconnect Method  
 This method updates the data source. It creates a new [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object and attaches the given **Recordset**.  
  
### Arguments  
 ***conn***  The connection string.  
  
 ***args***  The arguments for the handler.  
  
 ***pRS***  A **Recordset** object.  
  
## msdfhdl.idl  
 This is the interface definition for **IDataFactoryHandler** that appears in the **msdfhdl.idl** file.  
  
```cpp
[  
  uuid(D80DE8B3-0001-11d1-91E6-00C04FBBBFB3),  
  version(1.0)  
]  
library MSDFHDL  
{  
    importlib("stdole32.tlb");  
    importlib("stdole2.tlb");  
  
    // TLib : Microsoft ActiveX Data Objects 2.0 Library  
    // {00000200-0000-0010-8000-00AA006D2EA4}  
    #ifdef IMPLIB  
    importlib("implib\\x86\\release\\ado\\msado15.dll");  
    #else  
    importlib("msado20.dll");  
    #endif  
  
    [  
      odl,  
      uuid(D80DE8B5-0001-11d1-91E6-00C04FBBBFB3),  
      version(1.0)  
    ]  
    interface IDataFactoryHandler : IUnknown  
    {  
HRESULT _stdcall GetRecordset(  
      [in] BSTR conn,  
      [in] BSTR args,  
      [in] BSTR query,  
      [out, retval] _Recordset **ppRS);  
  
// DataFactory will use the ActiveConnection property  
// on the Recordset after calling Reconnect.  
   HRESULT _stdcall Reconnect(  
      [in] BSTR conn,  
      [in] BSTR args,  
      [in] _Recordset *pRS);  
    };  
};  
```  
  
## See Also  
 [Customization File Connect Section](../../../ado/guide/remote-data-service/customization-file-connect-section.md)   
 [Customization File Logs Section](../../../ado/guide/remote-data-service/customization-file-logs-section.md)   
 [Customization File SQL Section](../../../ado/guide/remote-data-service/customization-file-sql-section.md)   
 [Customization File UserList Section](../../../ado/guide/remote-data-service/customization-file-userlist-section.md)   
 [DataFactory Customization](../../../ado/guide/remote-data-service/datafactory-customization.md)   
 [Required Client Settings](../../../ado/guide/remote-data-service/required-client-settings.md)   
 [Understanding the Customization File](../../../ado/guide/remote-data-service/understanding-the-customization-file.md)


