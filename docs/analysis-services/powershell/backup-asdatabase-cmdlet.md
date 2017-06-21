---
title: "Backup-ASDatabase cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 03d58a82-021c-4e13-b265-c084f42a8bb2
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Backup-ASDatabase cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

  Backup an Analysis Services multidimensional or tabular database to an Analysis Services backup (.abf) file.  

>[!NOTE] 
>This article may contain outdated information and examples.
  
## Syntax  
 `Backup-ASDatabase [-BackupFile] <string> [-Name] <string> [-AllowOverwrite <SwitchParameter>] [-BackupRemotePartitions <SwitchParameter>] [-ApplyCompression <SwitchParameter>] [-FilePassword <SecureString>] [-Locations <Microsoft.AnalysisServices.BackupLocation[]>] [-Server <string>] [-Credential <PSCredential>] [<CommonParameters>]`  
  
 `Backup-ASDatabase –Database <Microsoft.AnalysisServices.Database> [-AllowOverwrite <SwitchParameter>] [-BackupRemotePartitions <SwitchParameter>] [-ApplyCompression <SwitchParameter>] [-FilePassword <SecureString>] [-Locations <Microsoft.AnalysisServices.BackupLocation[]>] [-Server <string>] [-Credential <PSCredential>] [<CommonParameters>]`  
  
## Description  
 Enables an Analysis Services system administrator to back up a multidimensional or tabular database to a backup file. If you do not specify a location, the default backup location that was specified during setup is used.  
  
 Files that you back up can be encrypted. Use –FilePassword to encrypt the file. When restoring the file later, you must provide the same password that you specified to encrypt it.  
  
 This cmdlet supports the –Credential parameter, which can be used if you configured the Analysis Services instance for HTTP access. The –Credential parameter takes a PSCredential object that provides a Windows user identity. IIS will then impersonate this user when connecting to Analysis Services. The identity must have system administrator permissions on the Analysis Services instance to perform the backup.  
  
## Parameters  
  
### -BackupFile \<string>  
 Specifies the path and name of the backup file. If you specify just a file name without a path, the default backup location will be used. This parameter is only used with the –Name parameter.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Name \<string>  
 Specifies the Analysis Services database to be backed up. You can specify a database using either the –Database parameter or the –Name parameter if you want to pass in the name as a string.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|1|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -AllowOverwrite \<SwitchParameter>  
 Overwrites a backup file of the same name.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -BackupRemotePartitions \<SwitchParameter>  
 Specifies whether remote partitions will be included in the backup.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -ApplyCompression\<SwitchParameter>  
 Specifies whether to compress the backup file.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -FilePassword \<SecureString>  
 Specifies a password to be used with backup file encryption.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Locations \<Microsoft.AnalysisServices.BackupLocation[]>  
 Specifies the location where the backup file will be stored.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Server \<string>  
 Specifies the Analysis Services instance to which the cmdlet will connect and execute. If no server name is provided, a connection will be made to localhost. For default instances, specify just the server name. For named instances, use the format servername\instancename. For HTTP connections, use the format http[s]://server[:port]/virtualdirectory/msmdpump.dll.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|localhost|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Credential \<PSCredential>  
 Specifies a PSCredential object that provides a Windows user name and password. Specify this parameter only if the Analysis Services instance is configured for HTTP access, using Basic authentication. For native connections that use integrated security, this parameter is ignored.  
  
 If this parameter is present, the credentials that it provides are appended to the connection string. IIS will impersonate this user identity when connecting to Analysis Services. If no credentials are specified, the default windows account of the user who is running the tool will be used.  
  
 To use this parameter, first create a PSCredential object using Get-Credential to specify the username and password (for example, `$Cred=Get-Credential “adventure-works\admin”`. You can then pipe this object to the –Credential parameter `(-Credential:$Cred`).  
  
For more information about HTTP access, see [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md).  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|True (ByValue)|  
|Accept wildcard characters?|false|  
  
### -Database \<Microsoft.AnalysisServices.Database[]>  
 Specifies an Analysis Services database object to be backed up. You can specify a database using either the -Database parameter or the –Name parameter. Use –Database if you want to pipe in the database name.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|true|  
|Accept wildcard characters?|false|  
  
### \<CommonParameters>  
 This cmdlet supports the common parameters: -Verbose, -Debug, -ErrorAction, -ErrorVariable, -OutBuffer, and -OutVariable. For more information, see [About_CommonParameters](http://go.microsoft.com/fwlink/?linkID=227825).  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs|Microsoft.AnalysisServices.Database<br /><br /> You can pipe multiple databases that you want to back up, for example all of the databases of a specific instance.|  
|Outputs|None.|  
  
## Example 1  
  
```  
PS SQLSERVER:\SQLAS\Localhost\default >backup-asdatabase awdb-20110930.abf “Adventure Works” -AllowOverwrite -ApplyCompression  
```  
  
 This command backs up the Adventure Works database to an .abf file at the default backup location. If an existing file of the same name exists at the location, it is overwritten.  
  
## Example 2  
  
```  
PS SQLSERVER:\SQLAS\Localhost\default >$AWDB = get-item “databases\Adventure Works”   
PS SQLSERVER:\SQLAS\Localhost\default >Backup-asdatabase –database:$AWDB –AllowOverwrite  
```  
  
 This command uses –Database instead of -Backupfile and -Name. Use the –Database parameter when you want to pipe the database name to the cmdlet.  
  
## Example 3  
  
```  
PS SQLSERVER:\SQLAS\Localhost\default\databases >dir * | backup-asdatabase  
```  
  
 This command backs up all of the databases on the local server.  
  
## Example 4  
  
```  
PS SQLSERVER:\SQLAS\Localhost\default > $pwd = read-host –AsSecureString –Prompt “Password”   
PS SQLSERVER:\SQLAS\Localhost\default > $pwd -is [System.IDisposable]   
PS SQLSERVER:\SQLAS\Localhost\default > backup-asdatabase –backupfile test.abf –name contoso_retail –filepassword:$pwd server myremoteserver  
PS SQLSERVER:\SQLAS\Localhost\default >$pwd.Dispose()  
PS SQLSERVER:\SQLAS\Localhost\default >Remove-Variable –Name pwd  
```  
  
 Lines 1 and 2 are used to prompt for a password that will be used to encrypt the file.  
  
 Line 3 backs up the Contoso_Retail sample database on the remote Analysis Services server to an Analysis Services backup file named test.abf, also on the remote server. The file is saved to the default backup folder of the default instance.  
  
 Lines 4 and 5 remove the password.  
  
  
  