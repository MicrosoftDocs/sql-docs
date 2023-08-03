---
title: "The OLE DB Provider for Internet Publishing"
description: "The OLE DB Provider for Internet Publishing"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "OLE DB provider for Internet publishing [ADO]"
  - "ADO, Internet publishing"
  - "publishing to Internet [ADO]"
  - "Internet publishing [ADO]"
  - "providers [ADO], OLE DB provider for Internet publishing"
---
# The OLE DB Provider for Internet Publishing
The ADO [Record](../../../ado/reference/ado-api/record-object-ado.md) and [Stream](../../../ado/reference/ado-api/stream-object-ado.md) objects can be used with the Microsoft OLE DB Provider for Internet Publishing (Internet Publishing Provider) to access and manipulate resources, such as Web folders or files served by Microsoft FrontPage. With ADO, you can specify the source of a **Record**, **Stream**, or [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) to be a URL. You can then upload, download, move, copy, and delete resources, or directly manipulate resource properties.  
  
 For example code that uses **Records** and **Streams** with the Internet Publishing Provider, see the [Internet Publishing Scenario](../../../ado/guide/data/internet-publishing-scenario.md).  
  
 The Internet Publishing Provider is installed with Microsoft Windows 2000. Earlier versions of the Internet Publishing Provider are also available with Microsoft Office 2000 and Microsoft Internet Explorer 5.0.  
  
 There are three ways to connect ADO to the Internet Publishing Provider:  
  
-   Specify "URL=" in the connection string. For example:  
  
    ```  
    objConn.Open "URL=https://servername"  
    ```  
  
-   Specify Msdaipp.dso for the *Provider* keyword of the connection string. For example:  
  
    ```  
    objConn.Open "provider=MSDAIPP.DSO;data source=https://servername"  
    ```  
  
-   Specify Msdaipp.dso for the [Provider](../../../ado/reference/ado-api/provider-property-ado.md) property of the [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object. For example:  
  
    ```  
    objConn.Provider = "MSDAIPP.DSO"  
    objConn.Open "https://servername"  
    ```  
  
> [!NOTE]
>  If Msdaipp.dso is explicitly specified as the value of the provider, either with the *Provider* connection string keyword or the **Provider** property, you cannot use "URL=" in the connection string. If you do, an error will occur. Instead, simply specify the URL as shown earlier.  
  
 For more specific information about the Internet Publishing Provider, see [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md), or the provider documentation provided with the source application with which the OLE DB Provider for Internet Publishing was installed: Windows 2000, Office 2000, or Internet Explorer 5.0.
