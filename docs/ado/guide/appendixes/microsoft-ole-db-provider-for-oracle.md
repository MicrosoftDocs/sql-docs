---
title: "Microsoft OLE DB Provider for Oracle"
description: "Microsoft OLE DB Provider for Oracle Overview"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "providers [ADO], OLE DB provider for Oracle"
  - "OLE DB provider for Oracle [ADO]"
  - "Oracle provider [ADO]"
---
# Microsoft OLE DB Provider for Oracle Overview
> [!IMPORTANT]
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use Oracle's OLE DB provider.

 The Microsoft OLE DB Provider for Oracle allows ADO to access Oracle databases.

## Connection String Parameters
 To connect to this provider, set the *Provider* argument of the [ConnectionString](../../reference/ado-api/connectionstring-property-ado.md) property to:

```vb
MSDAORA
```

 Reading the [Provider](../../reference/ado-api/provider-property-ado.md) property will return this string as well.

 If a join query with a keyset or dynamic cursor is executed in an Oracle database, an error occurs. Oracle only supports a static read-only cursor.

## Typical Connection String
 A typical connection string for this provider is:

```vb
"Provider=MSDAORA;Data Source=serverName;User ID=MyUserID; Password=MyPassword;"
```

 The string consists of these keywords:

|Keyword|Description|
|-------------|-----------------|
|**Provider**|Specifies the OLE DB Provider for Oracle.|
|**Data Source**|Specifies the name of a server.|
|**User ID**|Specifies the user name.|
|**Password**|Specifies the user password.|

> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.

## Provider-Specific Connection Parameters
 The provider supports several provider-specific connection parameters in addition to those defined by ADO. As with the ADO connection properties, these provider-specific properties can be set via the [Properties](../../reference/ado-api/properties-collection-ado.md) collection of a [Connection](../../reference/ado-api/connection-object-ado.md) or as part of the **ConnectionString**.

 These parameters are fully described in the [OLE DB Programmer's Reference](/previous-versions/windows/desktop/ms713643(v=vs.85)). The [ADO Dynamic Property Index](../../reference/ado-api/ado-dynamic-property-index.md) provides a cross-reference between these parameter names and the corresponding OLE DB properties.

|Parameter|Description|
|---------------|-----------------|
|**Window Handle**|Indicates the window handle to use to prompt for additional information.|
|**Locale Identifier**|Indicates a unique 32-bit number (for example, 1033) that specifies preferences related to the user's language. These preferences indicate how dates and times are formatted, items are sorted alphabetically, strings are compared, and so on.|
|**OLE DB Services**|Indicates a bitmask that specifies OLE DB services to enable or disable.|
|**Prompt**|Indicates whether to prompt the user while a connection is being established.|
|**Extended Properties**|A string containing provider-specific, extended connection information. Use this property only for provider-specific connection information that cannot be described through the property mechanism.|

## See Also
 [ConnectionString Property (ADO)](../../reference/ado-api/connectionstring-property-ado.md)
 [Provider Property (ADO)](../../reference/ado-api/provider-property-ado.md)
 [Recordset Object (ADO)](../../reference/ado-api/recordset-object-ado.md)