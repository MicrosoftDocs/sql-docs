---
title: "Using ADO for Internet Publishing"
description: "Using ADO for Internet Publishing"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADO, Internet publishing"
  - "publishing to Internet [ADO]"
  - "Internet publishing [ADO]"
  - "urls [ADO]"
---
# Using ADO for Internet Publishing
[The OLE DB Provider for Internet Publishing](../../../ado/guide/data/the-ole-db-provider-for-internet-publishing.md) shows a specific example of accessing heterogeneous data with ADO. Although the examples in this section will be specific to using the Internet Publishing Provider, the principles demonstrated should be similar when using ADO with other providers to heterogeneous data, such as a provider to an e-mail store.  
  
## URLs  
 Uniform Resource Locators (URLs) can be used as an alternative to connection strings and command text to specify data sources and the location of files and directories. You can use URLs with the existing [Connection](../../../ado/reference/ado-api/connection-object-ado.md) and **Recordset** objects and with the **Record** and **Stream** objects.  
  
 For more information about how to use URLs, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
## Record Fields  
 The distinguishing difference between heterogeneous data and homogeneous data is that for the former, each row of data, or **Record**, can have a different set of columns, or **Fields**. For homogeneous data, each row has the same set of columns. For more information about the fields specific to the Internet Publishing Provider, see [Records and Provider-Supplied Extra Fields](../../../ado/guide/data/records-and-provider-supplied-fields.md).  
  
### Appending New Fields  
 Several ADO objects have been enhanced to work together with **Record** and **Stream** objects.  
  
-   The [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection [Append](../../../ado/reference/ado-api/append-method-ado.md) method, which creates and adds a [Field](../../../ado/reference/ado-api/field-object.md) object to the collection, can also specify the value of the **Field**.  
  
-   The [Update](../../../ado/reference/ado-api/update-method.md) method finalizes the addition or deletion of fields to the collection.  
  
-   As a shortcut and alternative to the **Append** method, you can create fields by assigning a value to an undefined or previously deleted field.  
  
 This section contains the following topics.  
  
-   [The OLE DB Provider for Internet Publishing](../../../ado/guide/data/the-ole-db-provider-for-internet-publishing.md)  
  
-   [Internet Publishing Scenario](../../../ado/guide/data/internet-publishing-scenario.md)  
  
-   [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md)  
  
-   [Records and Provider-Supplied Fields](../../../ado/guide/data/records-and-provider-supplied-fields.md)  
  
## See Also  
 [Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)   
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)   
 [ADO History](../../../ado/guide/ado-history.md)
