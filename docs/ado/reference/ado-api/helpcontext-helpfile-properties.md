---
title: "HelpContext, HelpFile Properties"
description: "HelpContext, HelpFile Properties"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Error::GetHelpContext"
  - "Error::GetHelpFile"
  - "Error::get_HelpFile"
  - "Error::get_HelpContext"
  - "Error::HelpContext"
  - "Error::HelpFile"
helpviewer_keywords:
  - "HelpContext property [ADO]"
  - "HelpFile property [ADO]"
apitype: "COM"
---
# HelpContext, HelpFile Properties
Indicates the Help file and topic associated with an [Error](./error-object.md) object.  
  
## Return Values  
  
-   **HelpContextID** Returns a context ID, as a **Long** value, for a topic in a Help file.  
  
-   **HelpFile** Returns a **String** value that evaluates to a fully resolved path to a Help file.  
  
## Remarks  
 If a Help file is specified in the **HelpFile** property, the **HelpContext** property is used to automatically display the Help topic it identifies. If there is no relevant help topic available, the **HelpContext** property returns zero and the **HelpFile** property returns a zero-length string ("").  
  
## Applies To  
 [Error Object](./error-object.md)  
  
## See Also  
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VB)](./description-helpcontext-helpfile-nativeerror-number-source-example-vb.md)   
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VC++)](./description-helpcontext-helpfile-nativeerror-number-source-example-vc.md)   
 [Description Property](./description-property.md)   
 [Number Property (ADO)](./number-property-ado.md)   
 [Source Property (ADO Error)](./source-property-ado-error.md)