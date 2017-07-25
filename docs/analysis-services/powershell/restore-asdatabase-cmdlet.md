---
title: "Restore-ASDatabase cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 8ab7a2d0-679c-40e6-b9b9-042184b2dfc9
caps.latest.revision: 11
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Restore-ASDatabase cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

  Restores a Multidimensional or Tabular database backup (.abf) file to an Analysis Services instance.  

>[!NOTE] 
>This article may contain outdated information and examples. Use the Get-Help cmdlet for the latest.
  
## Syntax  
 `Restore-ASDatabase [-RestoreFile] <string> [-Name] <string> [-AllowOverwrite <SwitchParameter>] Locations <Microsoft.AnalysisServices.RestoreLocation[]>] [-Security <Microsoft.AnalysisServices.RestoreSecurity>] [-Password <System.SecureString>] [-StorageLocation <System.string>] [-Server <string>] [-Credential <PSCredential>] [<CommonParameters>]`  
  
## Description  
 Enables an Analysis Services system administrator to restore a Multidimensional or Tabular database from a backup (.abf) file to a local or remote server instance. If the file you are restoring was encrypted, use –FilePassword to provide the password that is used to decrypt the file.  
  
 This cmdlet supports the –Credential parameter, which can be used if you configured the Analysis Services instance for HTTP access. The –Credential parameter takes a PSCredential object that provides a Windows user identity. IIS will then impersonate this user when connecting to Analysis Services. The identity must have system administrator permissions on the Analysis Services instance to restore the file.  
  
## Parameters  
  
### -RestoreFile \<string>  
 Specifies the path and name of the file to restore. If you specify just the file name, without a path, the default backup location is assumed.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Name \<string>  
 Specifies the Analysis Services database to be restored.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|1|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -AllowOverwrite \<SwitchParameter>  
 Overwrites a database that uses the same name and location.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Locations \<Microsoft.AnalysisServices.RestoreLocation[]>  
 Specifies the remote location of the partitions to be restored.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Security \<Microsoft.AnalysisServices.RestoreSecurity>  
 Represents the security settings used for the restore operation. Valid values are CopyAll, SkipMembership, IgnoreSecurity. CopyAll restores roles and membership. SkipMembership recreates just the role. IgnoreSecurity restores the database, without roles.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Password \<SecureString>  
 Specifies a password to be used to restore an encrypted backup file. You must specify the password that was originally used to encrypt the file.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -StorageLocation \<string>  
 Specifies the database storage location. This is the location of the database files on the file system. Set this parameter if you are not using the default location, which is the backup folder of the target instance.  
  
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
  
### \<CommonParameters>  
 This cmdlet supports the common parameters: -Verbose, -Debug, -ErrorAction, -ErrorVariable, -OutBuffer, and -OutVariable. For more information, see [About_CommonParameters](http://go.microsoft.com/fwlink/?linkID=227825).  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs|System.string<br /><br /> You can pipe in string values to the cmdlet.|  
|Outputs|None.|  
  
## Example 1  
  
```  
PS SQLSERVER:\SQLAS\localhost\default> restore-asdatabase awtest.abf testawrestoredb –security:CopyAll  
```  
  
 This command restores an Analysis Services backup file (awtest.abf) in the local backup folder to the local Analysis Services default instance. The database name does not have to exist; in this case, the database name is specified as part of the restore operation. Adding –Security:CopyAll populates roles and role membership from the backup database to the new restored database.  
  
## Example 2  
  
```  
PS SQLSERVER:\SQLAS\Localhost\default > $pwd = read-host –AsSecureString –Prompt “Password”   
PS SQLSERVER:\SQLAS\Localhost\default > $pwd -is [System.IDisposable]   
PS SQLSERVER:\SQLAS\localhost\default> restore-asdatabase –restorefile testdb.abf –name AWTEST2 –password:$pwd  
PS SQLSERVER:\SQLAS\Localhost\default >$pwd.Dispose()  
PS SQLSERVER:\SQLAS\Localhost\default >Remove-Variable –Name pwd  
```  
  
 Lines 1 and 2 are used to prompt for the password that was used to encrypt the file.  
  
 Line 3 restores an encrypted Analysis Services backup file (testdb.abf) from a local backup folder of an Analysis Services default instance.  
  
 Lines 4 and 5 remove the password.  
  
## Example 3 (remote scenario)  
 This example demonstrates how to restore a local backup file from a file share to a remote instance of Analysis Services. In this example, a backup file is restored as a database named **internetsales** on a default instance of Analysis Services, on a computer named **ssas-aw-srv01**.  
  
 The backup file is located on a network share, with public read access. The remote Analysis Services instance must have read permission to the file. The file location must be a network share (no drives).  
  
 Notice that this example does not rely on the SQLAS provider. You can run the cmdlet as a standalone command.  
  
```  
PS SQLSERVER:\> restore-asdatabase -restorefile "\\FileServer01\DBFiles\InternetSalesTabular.abf" -name "internetsales  
" -server "SSAS-AW-SRV01"  
```  
  
## Example 4  
  
```  
PS SQLSERVER:\SQLAS\localhost\default> restore-asdatabase –restorefile “\\myremoteserver\backups\testdb.abf” –name Contoso_Retail –server myremoteserver –storagelocation “\\myremoteserver\restoreDBFiles”  
```  
  
 This command restores an encrypted Analysis Services backup file (testdb.abf) in a remote backup folder on a remote Analysis Services default instance. The –StorageLocation parameter is used to place the database files in a non-default location, in this case a file shared named restoreDBfiles.  
  
