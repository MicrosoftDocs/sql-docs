---
title: "XML Security Considerations"
description: "XML Security Considerations"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "XML security in ADO"
---
# XML Security Considerations
The ADO Save and Open methods on the Recordset object are not considered safe operations to run in Internet Explorer. Thus, if these methods are used in a script code that is running in an application or control that is hosted in a browser, the security configuration of the browser will have an effect on its behavior.  
  
 Internet Explorer 5 provides security restrictions for such operations by default in the Internet zones. Under that configuration, the Recordset cannot make any access to the local file system on the client or access any data sources outside the domain of the server from which the page has been downloaded. Specifically, when running inside the browser host, a Recordset can be saved back to a file only if it is on the same server from which the page was downloaded. Similarly, you can open a Recordset by loading it from a file only if that file is on the same server from which the page was downloaded.  
  
## See Also  
 [Persisting Records in XML Format](./persisting-records-in-xml-format.md)