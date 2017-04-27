---
title: "Lesson 1: Create stored access policy and shared access signature | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-backup-restore"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: 41674d9d-8132-4bff-be4d-85a861419f3d
caps.latest.revision: 22
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Lesson 1: Create stored access policy and shared access signature
In this lesson, you will use an [Azure PowerShell](https://azure.microsoft.com/en-us/documentation/articles/powershell-install-configure/) script to create a shared access signature on an Azure blob container using a stored access policy.  
  
> [!NOTE]  
> This script is written using Azure PowerShell 5.0.10586.  
  
A shared access signature is a URI that grants restricted access rights to containers, blobs, queues, or tables. A stored access policy provides an additional level of control over shared access signatures on the server side including revoking, expiring, or extending access. When using this new enhancement, you need to create a policy on a container with at least read, write, and list rights.  
  
You can create a stored access policy and a shared access signature by using Azure PowerShell, the Azure Storage SDK, the Azure REST API, or a 3rd party utility. This tutorial demonstrates how to use an Azure PowerShell script to complete this task. The script uses the Resource Manager deployment model and creates  the following new resources  
  
-   Resource group  
  
-   Storage account  
  
-   Azure blob container  
  
-   SAS policy  
  
This script starts by declaring a number of variables to specify the names for the above resources and the names of the following required input values:  
  
-   A prefix name used in naming other resource objects  
  
-   Subscription name  
  
-   Data center location  
  
> [!NOTE]  
> This script is written in such a way to allow you to use either existing ARM or classic deployment model resources.  
  
The script completes by generating the appropriate CREATE CREDENTIAL statement that you will use in [Lesson 2: Create a SQL Server credential using a shared access signature](../relational-databases/lesson-2-create-a-sql-server-credential-using-a-shared-access-signature.md). This statement is copied to your clipboard for you and is output to the console for you to see.  
  
To create a policy on container and generate a Shared Access Signature (SAS) key, follow these steps:  
  
1.  Open Window PowerShell or Windows PowerShell ISE (see version requirements above).  
  
2.  Edit and then execute the following script.  
  
    ```  
    \<#   
    This script uses the Azure Resource model and creates a new ARM storage account.  
    Modify this script to use an existing ARM or classic storage account   
    using the instructions in comments within this script  
    #>  
    # Define global variables for the script  
    $prefixName = '<a prefix name>'  # used as the prefix for the name for various objects  
    $subscriptionName='<your subscription name>'   # the name  of subscription name you will use  
    $locationName = '<a data center location>'  # the data center region you will use  
    $storageAccountName= $prefixName + 'storage' # the storage account name you will create or use  
    $containerName= $prefixName + 'container'  # the storage container name to which you will attach the SAS policy with its SAS token  
    $policyName = $prefixName + 'policy' # the name of the SAS policy  
  
    \<#   
    Using Azure Resource Manager deployment model  
    Comment out this entire section and use the classic storage account name to use an existing classic storage account  
    #>  
  
    # Set a variable for the name of the resource group you will create or use  
    $resourceGroupName=$prefixName + 'rg'   
  
    # adds an authenticated Azure account for use in the session   
    Login-AzureRmAccount    
  
    # set the tenant, subscription and environment for use in the rest of   
    Set-AzureRmContext -SubscriptionName $subscriptionName   
  
    # create a new resource group - comment out this line to use an existing resource group  
    New-AzureRmResourceGroup -Name $resourceGroupName -Location $locationName   
  
    # Create a new ARM storage account - comment out this line to use an existing ARM storage account  
    New-AzureRmStorageAccount -Name $storageAccountName -ResourceGroupName $resourceGroupName -Type Standard_RAGRS -Location $locationName   
  
    # Get the access keys for the ARM storage account  
    $accountKeys = Get-AzureRmStorageAccountKey -ResourceGroupName $resourceGroupName -Name $storageAccountName  
  
    # Create a new storage account context using an ARM storage account  
    $storageContext = New-AzureStorageContext -StorageAccountName $storageAccountName -StorageAccountKey $accountKeys[0].Value 
  
    \<#  
    Using the Classic deployment model  
    Use the following four lines to use an existing classic storage account  
    #>  
    #Classic storage account name  
    #Add-AzureAccount  
    #Select-AzureSubscription -SubscriptionName $subscriptionName #provide an existing classic storage account  
    #$accountKeys = Get-AzureStorageKey -StorageAccountName $storageAccountName  
    #$storageContext = New-AzureStorageContext -StorageAccountName $storageAccountName -StorageAccountKey $accountKeys.Primary  
  
    # The remainder of this script works with either the ARM or classic sections of code above  
  
    # Creates a new container in blob storage  
    $container = New-AzureStorageContainer -Context $storageContext -Name $containerName  
    $cbc = $container.CloudBlobContainer  
  
    # Sets up a Stored Access Policy and a Shared Access Signature for the new container  
    $permissions = $cbc.GetPermissions();  
    $policyName = $policyName  
    $policy = new-object 'Microsoft.WindowsAzure.Storage.Blob.SharedAccessBlobPolicy'  
    $policy.SharedAccessStartTime = $(Get-Date).ToUniversalTime().AddMinutes(-5)  
    $policy.SharedAccessExpiryTime = $(Get-Date).ToUniversalTime().AddYears(10)  
    $policy.Permissions = "Read,Write,List,Delete"  
    $permissions.SharedAccessPolicies.Add($policyName, $policy)  
    $cbc.SetPermissions($permissions);  
  
    # Gets the Shared Access Signature for the policy  
    $policy = new-object 'Microsoft.WindowsAzure.Storage.Blob.SharedAccessBlobPolicy'  
    $sas = $cbc.GetSharedAccessSignature($policy, $policyName)  
    Write-Host 'Shared Access Signature= '$($sas.Substring(1))''  
  
    # Outputs the Transact SQL to the clipboard and to the screen to create the credential using the Shared Access Signature  
    Write-Host 'Credential T-SQL'  
    $tSql = "CREATE CREDENTIAL [{0}] WITH IDENTITY='Shared Access Signature', SECRET='{1}'" -f $cbc.Uri,$sas.Substring(1)   
    $tSql | clip  
    Write-Host $tSql  
  
    ```  
  
3.  After the script completes, the CREATE CREDENTIAL statement will be in your clipboard for use in the next lesson.  
  
**Next Lesson:**  
  
[Lesson 2: Create a SQL Server credential using a shared access signature](../relational-databases/lesson-2-create-a-sql-server-credential-using-a-shared-access-signature.md)  
  
## See Also  
[Shared Access Signatures, Part 1: Understanding the SAS Model](https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-shared-access-signature-part-1/)  
[Create Container](https://msdn.microsoft.com/library/azure/dd179468.aspx)  
[Set Container ACL](https://msdn.microsoft.com/library/azure/dd179391.aspx)  
[Get Container ACL](https://msdn.microsoft.com/library/azure/dd179469.aspx)  
  
  
  

