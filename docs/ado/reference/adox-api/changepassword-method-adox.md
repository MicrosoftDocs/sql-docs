---
title: "ChangePassword Method (ADOX)"
description: "ChangePassword Method (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_User25::raw_ChangePassword"
  - "_User25::ChangePassword"
helpviewer_keywords:
  - "ChangePassword method [ADOX]"
apitype: "COM"
---
# ChangePassword Method (ADOX)
Changes the password for a [user](./user-object-adox.md) account.  
  
## Syntax  
  
```  
  
User.ChangePassword OldPassword, NewPassword  
```  
  
#### Parameters  
 *OldPassword*  
 A **String** value that specifies the user's existing password. If the user doesn't currently have a password, use an empty string ("") for *OldPassword*.  
  
 *NewPassword*  
 A **String** value that specifies the new password.  
  
## Remarks  
 For security reasons, the old password must be specified in addition to the new password.  
  
 An error will occur if the provider does not support the administration of trustee properties.  
  
## Applies To  
 [User Object (ADOX)](./user-object-adox.md)  
  
## See Also  
 [Groups and Users Append, ChangePassword Methods Example (VB)](./groups-and-users-append-changepassword-methods-example-vb.md)