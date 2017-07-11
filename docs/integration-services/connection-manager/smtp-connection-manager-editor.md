---
title: "SMTP Connection Manager Editor | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.smtpconnection.f1"
helpviewer_keywords: 
  - "SMTP Connection Manager Editor"
ms.assetid: 2693de0d-b04d-4325-a856-ce667d2b8aa1
caps.latest.revision: 37
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# SMTP Connection Manager Editor
  Use the **SMTP Connection Manager Editor** dialog box to specify a Simple Mail Transfer Protocol (SMTP) server.  
  
 To learn more about the SMTP connection manager, see [SMTP Connection Manager](../../integration-services/connection-manager/smtp-connection-manager.md).  
  
## Options  
 **Name**  
 Provide a unique name for the connection manager.  
  
 **Description**  
 Describe the connection manager. As a best practice, describe the connection manager in terms of its purpose, to make packages self-documenting and easier to maintain.  
  
 **SMTP server**  
 Provide the name of the SMTP server.  
  
 **Use Windows Authentication**  
 Select to send mail using an SMTP server that uses Windows Authentication to authenticate access to the server.  
  
> [!IMPORTANT]  
>  The SMTP connection manager supports only anonymous authentication and Windows Authentication. It does not support basic authentication.  
  
> [!NOTE]  
>  When using Microsoft Exchange as the SMTP server, you may need to set **Use Windows Authentication** to **True**. Exchange servers may be configured to disallow unauthenticated SMTP connections.  
  
 **Enable Secure Sockets Layer (SSL)**  
 Select to encrypt communication using Secure Sockets Layer (SSL) when sending e-mail messages.  
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)  
  
  