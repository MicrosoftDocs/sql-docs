---
title: "Microsoft OLE DB Provider for Internet Publishing | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/08/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "OLE DB provider for Internet publishing [ADO]"
  - "providers [ADO], OLE DB provider for Internet publishing"
  - "Internet Publishing provider [ADO]"
ms.assetid: 66a208d9-b580-4655-a41e-1d36e5b5bfca
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft OLE DB Provider for Internet Publishing Overview
The Microsoft OLE DB Provider for Internet Publishing allows ADO to access resources served by Microsoft FrontPage or Microsoft Internet Information Server. Resources include web source files such as HTML files, or Windows 2000 web folders.

## Connection String Parameters
 To connect to this provider, set the *Provider* argument of the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) property to:

```vb
MSDAIPP.DSO
```

 This value can also be set or read using the [Provider](../../../ado/reference/ado-api/provider-property-ado.md) property.

## Typical Connection String
 A typical connection string for this provider is:

```vb
"Provider=MSDAIPP.DSO;Data Source=ResourceURL;User ID=MyUserID;Password=MyPassword;"
```

 -or-

```vb
"URL=ResourceURL;User ID=MyUserID;Password=MyPassword;"
```

 The string consists of these keywords:

|Keyword|Description|
|-------------|-----------------|
|**Provider**|Specifies the OLE DB Provider for Internet Publishing.|
|**Data Source** -or- **URL**|Specifies the URL of a file or directory published in a Web Folder.|
|**User ID**|Specifies the user name.|
|**Password**|Specifies the user password.|

> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.

 If you set the *ResourceURL* value from the "URL=" in the connection string to an invalid value, by default the Internet Publishing Provider raises a dialog box to prompt for a valid value. This is undesirable behavior for a component in the middle tier of an application, because it suspends program execution until the dialog box is cleared and the client appears to freeze because it has not received a response from the component.

> [!NOTE]
>  If MSDAIPP.DSO is explicitly specified as the value of the provider, either with the *Provider* connection string keyword or the **Provider** property, you cannot use "URL=" in the connection string. If you do, an error will occur. Instead, simply specify the URL as shown in the topic [Using ADO with the OLE DB Provider for Internet Publishing](../../../ado/guide/data/the-ole-db-provider-for-internet-publishing.md).

## See Also
 [Internet Publishing Scenario](../../../ado/guide/data/internet-publishing-scenario.md)
 [The OLE DB Provider for Internet Publishing](../../../ado/guide/data/the-ole-db-provider-for-internet-publishing.md)
