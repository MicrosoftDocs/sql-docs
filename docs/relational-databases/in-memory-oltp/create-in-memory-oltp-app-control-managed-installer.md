---
title: "Create In-Memory OLTP App Control and Managed Installer Policies"
description: Learn how to create Windows Defender Application Control policies and designate the HKDLLGEN.exe (Hekaton DLL generator) as a Managed Installer for a SQL Server database with memory-optimized tables.
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 02/06/2025
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: how-to
---

# How to create In-Memory OLTP App Control and Managed Installer policies

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

SQL Server compiles and links a dynamic link library (DLL) for every native compiled table and stored procedure which has the native implementation of those objects in C code. While the In-Memory OLTP DLLs are generated dynamically, the files themselves might provide some challenges when there are compliance requirements that have code integrity enforcement as a criterion.

<a id="what-is-hkdllgen"></a>

## What is HKDLLGEN?

In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Cumulative Update 17 and later versions, a component known as the Hekaton DLL generator was added to the In-Memory OLTP feature. Without the new Hekaton DLL Generation (hkdllgen) process, the main SQL Server process converts T-SQL to C code and then launches compiler and linker processes to generate unsigned In-Memory OLTP DLLs. `Hkdllgen`, as an intermediate application, validates and accepts the output from SQL Server and then creates **signed** DLLs. In order to enforce Code Integrity policies for these DLLs, the `Hkdllgen` process would need to be designated as [Windows Defender Application Control (WDAC)](/windows/security/application-security/application-control/app-control-for-business/appcontrol-and-applocker-overview) Managed Installer.

The Hekaton DLL generator is the first step toward ensuring that regulatory compliance requirements such as [code integrity](/azure/security/fundamentals/code-integrity#code-integrity-as-an-authorization-gate) can be met with In-Memory OLTP generated DLLs. Code integrity in this context makes sure that DLLs which In-Memory OLTP generates are trusted by the operating system from the time that they're created until they're loaded and executed. The ability to designate the Hekaton DLL generator component as a Managed Installer allows the WDAC code integrity system to trust the DLLs that are generated and allows them to be used.

## How does a managed installer work?

Managed installer uses a special rule collection in [AppLocker](/windows/security/application-security/application-control/app-control-for-business/appcontrol-and-applocker-overview#AppLocker) to designate binaries that are trusted by your organization as an authorized source for application installation. When one of these trusted binaries runs, Windows monitors the binary's process (and any child processes it launches) and watches for files being written to disk. As files are written, a claim or tag is added to the file as originating from a managed installer.

With AppLocker, WDAC App Control can be configured to trust files that are installed by a managed installer by adding the **Enabled:Managed Installer** option to an App Control policy. When that option is set, App Control checks for managed installer origin information when determining whether or not to allow a binary to run. As long as there are no deny rules for the binary, App Control allows it to run based purely on its managed installer origin. AppLocker also controls the execution of executable files that are designated as a Managed Installer, but it doesn't offer a chain of trust for executables and DLLs like WDAC. In this article, we walk through how to designate and configure the `hkdllgen` process as a managed installer that can be used by both AppLocker and WDAC.

## Enable the Hekaton DLL generator

### Example

In this example, `sp_configure` is used to enable the Hekaton DLL generator option, which is called `external xtp dll gen util enabled`. A test database is created, along with a test memory optimized table.

1. Create a test database.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'external xtp dll gen util enabled', 1;
   RECONFIGURE;
   GO

   CREATE DATABASE HekatonDbForTesting ON
   PRIMARY (
       NAME = N'HekatonDbForTesting_Data',
       FILENAME = N'<path-to-data-directory>\HekatonDbForTesting_Data.mdf'
   ),
   FILEGROUP [HekatonDbForTestin_XTP_FG] CONTAINS MEMORY_OPTIMIZED_DATA (
       NAME = HekatonDbForTesting_XTP_CHKPOINT,
       FILENAME = N'<path-to-data-directory>\HekatonDbForTesting_XTP_CHKPOINT'
   )
   LOG ON (
       NAME = N'HekatonDbForTesting_log',
       FILENAME = N'<Path_To_Log_Directory>\HekatonDbForTesting_Log.ldf'
   );
   GO
   ```

1. Create a test table within the test database.

   ```sql
   USE HekatonDbForTesting;
   GO

   CREATE TABLE dbo.TestCustomerTable
   (
       CustomerId INT NOT NULL
           PRIMARY KEY NONCLUSTERED HASH WITH (BUCKET_COUNT = 1000000),
       FirstName NVARCHAR (50) NOT NULL,
       LastName NVARCHAR (50) NOT NULL
   )
   WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA);
   GO
   ```

1. A new zero-length file with a `.gen` extension is generated for each `.dll` within the `<path-to-data-directory>\xtp\<database_id>` subdirectory. The dlls are now signed.

## Steps to create In-Memory OLTP AppLocker and Managed Installer policies

The AppLocker policy creation UI in GPO Editor (**gpedit.msc**) and the AppLocker PowerShell cmdlets can't be directly used to create rules for the Managed Installer rule collection. However, you can use an XML or text editor to convert an EXE rule collection policy into a ManagedInstaller rule collection.

> [!IMPORTANT]  
> An AppLocker policy should exist before adding the Hekaton DLL Generation executable to a servers AppLocker Control policy configuration, otherwise, there's a risk that basic operating system functions might be blocked by Windows Defender. For more information about creation, testing, and maintenance of application control policies, see the [AppLocker deployment guide](/windows/security/application-security/application-control/app-control-for-business/applocker/applocker-policies-deployment-guide).

The remaining examples apply to **Windows Server 2022** and **Windows 11** and later versions.

To verify that at least an *exe* rule collection exists within the servers AppLocker Control policy configuration, execute the following PowerShell command:

```powershell
Get-AppLockerPolicy -Effective
```

Or to save the output of the effective policies to an XML file for viewing:

```powershell
Get-AppLockerPolicy -Effective -Xml > effective_app_policy.xml
```

The following steps walk through the process of creating and applying a policy that can be applied to a local server. A Managed Installer policy that is generated using these steps can be merged into a GPO wide policy and distributed to all SQL Server instances within an environment, or applied to a single server's local policy. We recommend that you work with a Domain Administrator to have the Code Integrity policy applied from the domain level.

1. Use [New-AppLockerPolicy](/powershell/module/applocker/new-applockerpolicy) to make an EXE rule for the file you're designating as a managed installer. This example creates a rule for Hekaton DLL generator using the Publisher rule type, but any AppLocker rule type can be used. You might need to reformat the output for readability.

   ```powershell
   #Change the current working path of the PowerShell command line or ISE to something other than the default (that is, C:\Temp). Retrieve SQL Server Path
   $SQLPath = Get-ItemProperty -Path 'HKLM:\SOFTWARE\Microsoft\MSSQLServer\Setup' -Name 'SQLPath'
   $FullPath = Join-Path -Path $SQLPath.SQLPath -ChildPath 'Binn\xtp'

   # Set an environment variable for the In-memory OLTP Path
   [System.Environment]::SetEnvironmentVariable('SQLPathWithXtp', $FullPath, 'Process')

   # Generate an AppLocker Policy for the HKDLLGEN.EXE in the current working directory. The Get-AppLockerFileInformation cmdlet will extract the    executables publisher information as well as generate a hash for the binary.
   Get-ChildItem -Path ${env:SQLPathWithXtp}'.\hkdllgen.exe' | Get-AppLockerFileInformation | New-AppLockerPolicy -RuleType Publisher -User Everyone    -Xml > AppLocker_HKDLLGEN_Policy.xml
   ```

1. Manually edit the `AppLocker_HKDLLGEN_Policy.xml` and change the following attribute values:

   - `RuleCollection Type` to `ManagedInstaller`
   - `EnforcementMode` to `AuditOnly`
   - `BinaryVersionRange LowSection` to `"*"` and `HighSection` to `"*"`

   Change:

   ```xml
   <RuleCollection Type="Exe" EnforcementMode="NotConfigured">
   ```

   to:

   ```xml
   <RuleCollection Type="ManagedInstaller" EnforcementMode="AuditOnly">
   ```

   Change:

   ```xml
   <BinaryVersionRange LowSection="2022.160.4175.1" HighSection="2022.160.4175.1"/>
   ```

   to:

   ```xml
   <BinaryVersionRange LowSection="*" HighSection="*"/>
   ```

1. Deploy the AppLocker managed installer configuration policy. You can either import the AppLocker policy and deploy with Group Policy or use a script to deploy the policy with the Set-AppLockerPolicy cmdlet as shown in the following PowerShell command.

   ```powershell
   #Enable the AppLocker Policy and merge with the existing policy that exists on the system.
   Set-AppLockerPolicy -XmlPolicy .\AppLocker_HKDLLGEN_Policy.xml -Merge -ErrorAction SilentlyContinue
   ```

1. If deploying the AppLocker policy via a PowerShell script, use the **appidtel.exe** utility from an Administrative command prompt to configure the AppLocker Application Identity service and AppLocker filter driver.

   ```cmd
   appidtel.exe start [-mionly]
   ```

## Enable the managed installer option in the Windows Defender Application Control for Business Wizard

For [Windows Defender Application Control (WDAC)](/windows/security/application-security/application-control/app-control-for-business/appcontrol-and-applocker-overview) to trust the DLLs that are generated by the `hkdllgen.exe` process, the **Enabled: Managed Installer** option must be specified in your App Control policy. This setting can be defined by using the [Set-RuleOption cmdlet](/powershell/module/configci/set-ruleoption) with Option 13.

Generate a code integrity policy file from one of the [WDAC Base Policy Wizard](/windows/security/application-security/application-control/app-control-for-business/design/appcontrol-wizard-create-base-policy#template-base-policies) template base policies.

Starting with the **Default Windows** policy provides fewer options, which are removed in this guide. More information about the Default Windows Mode and Allow Microsoft Mode policies can be accessed through the [Example App Control for Business base policies article](/windows/security/application-security/application-control/app-control-for-business/design/example-appcontrol-base-policies).

### Base template policy

:::image type="content" source="media/create-in-memory-oltp-app-control-managed-installer/1-base-template.png" alt-text="Screenshot of the WDAC Base Template screen." lightbox="media/create-in-memory-oltp-app-control-managed-installer/1-base-template.png":::

Once the Windows policy base template is selected, give the policy a name and choose where to save the App Control policy on disk.

### Select a policy type

Choose the **Multiple Policy Format** and **Base Policy** as the Policy Type

:::image type="content" source="media/create-in-memory-oltp-app-control-managed-installer/2-select-policy-type.png" alt-text="Screenshot of the WDAC Select Policy Type screen." lightbox="media/create-in-memory-oltp-app-control-managed-installer/2-select-policy-type.png":::

### Configure policy template

Enable only the Managed Installer, Update Policy without Rebooting, Unsigned System Integrity Policy, and User Mode Code Integrity policy rule options. Disable the other policy rule options. This can be done by pressing the slider button next to the policy rule titles.

The following table has a description of each policy rule, beginning with the left-most column. The [Policy rules article](/windows/security/application-security/application-control/app-control-for-business/design/select-types-of-rules-to-create#app-control-for-business-policy-rules) provides a fuller description of each policy rule.

| Rule option | Description |
| --- | --- |
| **Managed Installer** | Use this option to automatically allow applications installed by a software distribution solution, such as the Hekaton DLL generator, that has been defined as a managed installer. |
| **Update Policy without Rebooting** | Use this option to allow future App Control for Business policy updates to apply without requiring a system reboot. |
| **Unsigned System Integrity Policy** | Allows the policy to remain unsigned. When this option is removed, the policy must be signed and have UpdatePolicySigners added to the policy to enable future policy modifications. |
| **User Mode Code Integrity** | App Control for Business policies restrict both kernel-mode and user-mode binaries. By default, only kernel-mode binaries are restricted. Enabling this rule option validates user mode executables and scripts. |

:::image type="content" source="media/create-in-memory-oltp-app-control-managed-installer/3-configure-policy-template.png" alt-text="Screenshot of the Configure Policy Template screen." lightbox="media/create-in-memory-oltp-app-control-managed-installer/3-configure-policy-template.png":::

You should enable **Audit Mode** initially, because it allows you to test new App Control for Business policies before you enforce them. With audit mode, no application is blocked-instead the policy logs an event whenever an application outside the policy is started. For this reason, all templates have Audit Mode enabled by default.

### File rules

Remove all of the Policy Signing Rules from the list.

:::image type="content" source="media/create-in-memory-oltp-app-control-managed-installer/4-files-rules-removal.png" alt-text="Screenshot of the WDAC File Rules screen." lightbox="media/create-in-memory-oltp-app-control-managed-installer/4-files-rules-removal.png":::

*(Optional)* Add a Custom Publisher policy rule that would ensure that files such as **hkdllgen.exe** would be signed as a publisher.

:::image type="content" source="media/create-in-memory-oltp-app-control-managed-installer/5-custom-policy-rule.png" alt-text="Screenshot of the WDAC Custom policy screen." lightbox="media/create-in-memory-oltp-app-control-managed-installer/5-custom-policy-rule.png":::

The Publisher file rule type uses properties in the code signing certificate chain to base file rules.

:::image type="content" source="media/create-in-memory-oltp-app-control-managed-installer/6-custom-policy-rule-condition.png" alt-text="Screenshot of the WDAC Policy rule screen.":::

After selecting the **Create Rule** button, a single Policy Signing rule should exist.

:::image type="content" source="media/create-in-memory-oltp-app-control-managed-installer/7-policy-signing-rule-list.png" alt-text="Screenshot of the WDAC Policy signing rule list screen." lightbox="media/create-in-memory-oltp-app-control-managed-installer/7-policy-signing-rule-list.png":::

Deploy your App Control policy. See [Deploying App Control for Business policies](/windows/security/application-security/application-control/app-control-for-business/deployment/appcontrol-deployment-guide).

Once the policy is created, the new policy is written to the path that was chosen as the Policy File Location. The new binary version of the policy file name has the policy version appended to the end of the file name. The *policy*.cip file can be copied to the `C:\Windows\System32\CodeIntegrity\CiPolicies\Active` subdirectory on the SQL Server instance.

## Manually deploy a Code Integrity policy

In order to create a more streamlined Code Integrity policy, a more generic *policy*.xml file that was generated after completing the WDAC App Control Policy Wizard can be edited. This scenario can arise if the WDAC App Control Policy Wizard isn't executed on a SQL Server, but from a workstation. For example, a less customized Code Integrity policy file might look like:

```xml
   <?xml version="1.0" encoding="utf-8"?>
<SiPolicy xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="urn:schemas-microsoft-com:sipolicy" PolicyType="Base Policy">
  <VersionEx>10.0.5.0</VersionEx>
  <PlatformID>{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}</PlatformID>
  <PolicyID>{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}</PolicyID>
  <BasePolicyID>{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}</BasePolicyID>
  <Rules>
    <Rule>
      <Option>Enabled:Unsigned System Integrity Policy</Option>
    </Rule>
    <Rule>
      <Option>Enabled:UMCI</Option>
    </Rule>
    <Rule>
      <Option>Enabled:Audit Mode</Option>
    </Rule>
    <Rule>
      <Option>Enabled:Managed Installer</Option>
    </Rule>
    <Rule>
      <Option>Enabled:Update Policy No Reboot</Option>
    </Rule>
  </Rules>
  <EKUs>
    <!--EKU ID-->
  </EKUs>
  <FileRules>
    <!--FileAttrib ID -->
  </FileRules>
  <Signers />
  <SigningScenarios>
    <SigningScenario ID="ID_SIGNINGSCENARIO_KMCI" FriendlyName="Kernel Mode Signing Scenario" Value="131">
      <ProductSigners />
    </SigningScenario>
    <SigningScenario ID="ID_SIGNINGSCENARIO_UMCI" FriendlyName="User Mode Signing Scenario" Value="12">
      <ProductSigners />
    </SigningScenario>
  </SigningScenarios>
  <UpdatePolicySigners />
  <HvciOptions>0</HvciOptions>
</SiPolicy>
```

This example doesn't have a signed publisher rule and assumes that the policy file is using a local working directory (for example, `C:\Temp`) with a file name of `Hekaton_Custom_CIPolicy.xml`.

```powershell
#Create Windows Defender Application Control (WDAC) policy and set Option 13 (Enabled:Managed Installer) and Option 16 (Enabled:Update Policy No Reboot)
Set-CIPolicyIdInfo -FilePath C:\Temp\Hekaton_Custom_CIPolicy.xml -PolicyName "Hekaton Managed Installer Policy" -ResetPolicyID
Set-RuleOption -FilePath C:\Temp\Hekaton_Custom_CIPolicy.xml -Option 13
Set-RuleOption -FilePath C:\Temp\Hekaton_Custom_CIPolicy.xml -Option 16

# The App Control policy XML file in this example is located in the C:\Temp directory.
$AppControlPolicyXMLFile = 'C:\Temp\test\Hekaton_Custom_CIPolicy.xml'

# Retrieve the Policy ID from the App Control policy XML. This will be used as the binary file name that Code Integrity will use.
[xml]$AppControlPolicy = Get-Content -Path $AppControlPolicyXMLFile
$PolicyID = $AppControlPolicy.SiPolicy.PolicyID
$PolicyBinary = $PolicyID + ".cip"

# Convert the App Control policy XML to binary format and save it into the Active Code Integrity path.
ConvertFrom-CIPolicy -XmlFilePath $AppControlPolicyXMLFile -BinaryFilePath "C:\Windows\System32\CodeIntegrity\CiPolicies\Active\$PolicyBinary"
```

To apply the policy without rebooting the server and check the status of Code Integrity, run this PowerShell script:

```powershell
# Refresh the Code Integrity policy without a reboot of the system
Invoke-CimMethod -Namespace root\Microsoft\Windows\CI -ClassName PS_UpdateAndCompareCIPolicy -MethodName Update -Arguments @{FilePath = "C:\Windows\System32\CodeIntegrity\CiPolicies\Active\$PolicyBinary" }

# View the current status of WDAC Code Integrity.
# If WDAC is in Audit mode the "UserModeCodeIntegrityPolicyEnforcementStatus" will have a value of "1" for Audit mode. A value of "0" signifies that Code Integrity is not active.
Get-CimInstance -ClassName Win32_DeviceGuard -Namespace root\Microsoft\Windows\DeviceGuard | Format-List *codeintegrity*
```

## Verify that the Hekaton DLLs generated are trusted by Code Integrity

Once Code Integrity is operating in Audit mode or Active mode, the DLLs generated by the Hekaton DLL generator are trusted by Windows, and have an Extended Attributed add to the files.

A [Smartlocker](/windows/security/application-security/application-control/app-control-for-business/operations/configure-appcontrol-managed-installer#using-fsutil-to-query-extended-attributes-for-managed-installer-mi) claim is added as part of the metadata. This can be viewed by using the fsutil command from an Administrative command prompt. For example, selecting one of the In-memory OLTP dynamically generated files from the `\Data\xtp\<database_id>` folder, and executing the following command:

```console
fsutil file queryea "D:\SQL\MSSQL17.MSSQLSERVER\MSSQL\DATA\xtp\5\xtp_t_5_64719283_196202718557591_1.dll"
```

:::image type="content" source="media/create-in-memory-oltp-app-control-managed-installer/8-fsutil-verification.png" alt-text="Screenshot of fsutil output." lightbox="media/create-in-memory-oltp-app-control-managed-installer/8-fsutil-verification.png":::

## Remove Managed Installer feature

To remove the Managed Installer feature from the device, you need to remove the Managed Installer AppLocker policy from the device by following the instructions at [Delete an AppLocker rule: Clear AppLocker policies on a single system or remote systems](/windows/security/application-security/application-control/app-control-for-business/applocker/delete-an-applocker-rule#to-clear-applocker-policies-on-a-single-system-or-remote-systems).

## Related content

- [Automatically allow apps deployed by a managed installer with App Control for Business](/windows/security/application-security/application-control/app-control-for-business/design/configure-authorized-apps-deployed-with-a-managed-installer)
- [In-Memory OLTP overview and usage scenarios](overview-and-usage-scenarios.md)
- [A Guide to Query Processing for Memory-Optimized Tables](a-guide-to-query-processing-for-memory-optimized-tables.md)
- [Sample database for in-memory OLTP](sample-database-for-in-memory-oltp.md)
- [AppLocker deployment guide](/windows/security/application-security/application-control/app-control-for-business/applocker/applocker-policies-deployment-guide)
- [Deploying App Control for Business policies](/windows/security/application-security/application-control/app-control-for-business/deployment/appcontrol-deployment-guide)
