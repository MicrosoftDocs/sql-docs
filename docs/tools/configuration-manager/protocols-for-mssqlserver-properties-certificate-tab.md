---
title: "Protocols for MSSQLSERVER Properties (Certificate Tab)"
description: Select a certificate for SQL Server, or view certificate properties by using the Certificate tab on the Protocols for MSSQLSERVER Properties dialog box.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.computermgr.cert.general.f1"
helpviewer_keywords: 
  - "MSSQLSERVER property protocols"
ms.assetid: 776addd6-25f3-4875-9a71-064035787090
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016"
---
# Protocols for MSSQLSERVER Properties (Certificate Tab)
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  Use the **Certificate** tab on the **Protocols for MSSQLSERVER Properties** dialog box to select a certificate for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or to view the properties of a certificate. All fields are blank until a certificate is selected.  
  
 Certificates are stored locally for the users on the computer. To load a certificate for use by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must be running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager under the same user account as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
## Page Header  
 **View**  
 Provides access to additional details on the certificate. Not available until a certificate is selected in the **Certificate** box. For additional information on certificate details, see your [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows documentation.  
  
 **Clear**  
 Removes the selection from the **Certificate** box.  
  
 **Certificate**  
 Name of certificate as determined by security provider. Select a certificate to see the details in the properties grid.  
  
## Options  
 Expiration Date  
 The final date for the period in which the certificate is valid.  
  
 Friendly Name  
 A friendly or common name for the individual or certification authority to whom the certificate is issued.  
  
 Issued By  
 Information regarding the certification authority that issued the certificate.  
  
 Issued To  
 Information regarding the recipient of the certificate.  
  
  
