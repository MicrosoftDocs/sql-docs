---
description: "Appendix - 1 (MySQLToSQL)"
title: "Appendix - 1 (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 2d22766d-ff09-420d-ae7c-13b443e28bd0
author: cpichuka 
ms.author: cpichuka 
---
# Appendix - 1 (MySQLToSQL)
Quick view of the SSMA Console command line options:  
  
|Sl. No.|Switch|Required?|Switch Argument|Permitted Values|  
|-----------|----------|-------------|-------------------|--------------------|  
|1|-s/script|Yes|scriptfile|Valid XML file name.<br /><br />Console Script definition file.|  
|2|-v/variable|No|variablevaluefile|Valid XML file name.<br /><br />If variables are used in script file, then this file must be specified.|  
|3|-c/serverconnection|No|serverconnectionfile|Valid XML file name.<br /><br />This file contains server connection information.|  
|4|-x/xmloutput|No|xmloutputfile|This option indicates console output in the XML format. If this option is not specified, the default output is in TEXT format.<br /><br />If xmloutputfile is not specified, XML output is directed to STDOUT.<br /><br />Xmloutputfile is the name of the file to which the console output is written in the XML format.|  
|5|-l/log|No|logfile|Valid file name.|  
|6|-e/projectenvironment|No|projectenvironmentfolder|Valid folder name containing SSMA project environment files.|  
|7|-p/securepassword|No|-a/add {<server_id> [,...n] &#124; all} -c&#124;serverconnection  \<server-connection-file\> [-v&#124;variable \<variable-value-file\>] [-o/overwrite]<br /><br />or<br /><br />-a/add {<server_id> [,...n] &#124; all} -s&#124;script \<script-file\> [-v&#124;variable \<variable-value-file\>] [-o/overwrite]<br /><br />-r/remove {<server_id> [, ...n] &#124; all}<br /><br />-l/list<br /><br />-e/export {\<server-id\> [, ...n] &#124; all} <encrypted-password -file><br /><br />-i/import {\<server-id\> [, ...n] &#124; all} \<encrypted-password-file\>|If specified, this option must not be combined with any other options.<br /><br />server-id: A unique ID provided for a server {string}<br /><br />server-connection-file: server definition file (serverconnectionfile or scriptfile).<br /><br />variable-value-file: It is a variable definition file and used in server-connection-file.<br /><br />encrypted-password-file: It is a server passwords file encrypted using a user-specified pass-phrase.|  
|8|-?|No|Not Applicable|Not Applicable|  
  
## See Also  
[Executing the SSMA Console (MySQL)](./executing-the-ssma-console-mysqltosql.md)  
