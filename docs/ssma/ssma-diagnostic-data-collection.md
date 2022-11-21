---
title: SQL Server Migration Assistant (SSMA) usage and diagnostic data collection
description: Learn about usage and diagnostic data collection in SQL Server Migration Assistant.
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
author: cpichuka
ms.author: cpichuka
ms.reviewer: ""
ms.custom: ""
ms.date: 04/02/2021
---

# SSMA usage and diagnostic data collection

SQL Server Migration Assistant (SSMA) contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft.

## Collected data

SSMA may collect standard computer information and information about use and performance that may be transmitted to Microsoft and analyzed for purposes of improving the quality, security, and reliability of SSMA. SSMA doesn't collect your name, address, or any other data related to an identified or identifiable individual. For details, see the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement), and [SQL Server Privacy supplement](../sql-server/sql-server-privacy.md).
## Enable or disable usage and diagnostic data collection in SSMA

Following registry entry allows you to opt in or out of data collection:

RegEntry name = `DisableTelemetry`  
Entry type `STRING`: `True` is opt out; `False` or not present is opt in

Additionally, automatic checks for newer tool versions during start-up can be disabled by adding the following entry:

RegEntry name = `DisableAutoUpdate`  
Entry type `STRING`: `True` is opt out; `False` or not present is opt in

Depending on the migration source, the entry should be placed under one of the following registry keys:

- For SQL Server Migration Assistant for Access:

  Subkey = `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for Access`  
  Subkey (32-bit application installed on 64-bit OS) = `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for Access`  

- For SQL Server Migration Assistant for DB2:

  Subkey = `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for DB2`  
  Subkey (32-bit application installed on 64-bit OS) = `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for DB2`  

- For SQL Server Migration Assistant for MySQL:

  Subkey = `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for MySQL`  
  Subkey (32-bit application installed on 64-bit OS) = `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for MySQL`  

- For SQL Server Migration Assistant for Oracle:

  Subkey = `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for Oracle`  
  Subkey (32-bit application installed on 64-bit OS) = `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for Oracle`  

- For SQL Server Migration Assistant for Sybase:

  Subkey = `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for Sybase`  
  Subkey (32-bit application installed on 64-bit OS) = `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for Sybase`  
