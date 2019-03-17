---
title: "CLR Hosted Environment | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "type-safe code [CLR integration]"
  - "UNSAFE permission set"
  - "run-time environments [CLR integration]"
  - "common language runtime [SQL Server], about CLR integration"
  - "application domains [CLR integration]"
  - "host protection attributes [CLR integration]"
  - "managed code [SQL Server], common language runtime"
  - "permission sets [CLR integration]"
  - "reliability [CLR integration]"
  - "SAFE permission set"
  - "code access security [CLR integration]"
  - "EXTERNAL_ACCESS permission set"
  - "verifying type safety"
  - "scalability [CLR integration]"
  - "hosted environments [CLR integration]"
  - "HPAs [CLR integration]"
ms.assetid: d280d359-08f0-47b5-a07e-67dd2a58ad73
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# CLR Integration Architecture - CLR Hosted Environment
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] integration with the .NET Framework common language runtime (CLR) enables database programmers to use languages such as Visual C#, Visual Basic .NET, and Visual C++. Functions, stored procedures, triggers, data types, and aggregates are among the kinds of business logic that programmers can write with these languages.  
  
  The CLR features garbage-collected memory, pre-emptive threading, metadata services (type reflection), code verifiability, and code access security. The CLR uses metadata to locate and load classes, lay out instances in memory, resolve method invocations, generate native code, enforce security, and set run-time context boundaries.  
  
 The CLR and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] differ as run-time environments in the way they handle memory, threads, and synchronization. This topic describes the way in which these two run times are integrated so that all system resources are managed uniformly. This topic also covers the way in which CLR code access security (CAS) and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security are integrated to provide a reliable and secure execution environment for user code.  
  
## Basic Concepts of CLR Architecture  
 In the .NET Framework, a programmer writes in a high-level language that implements a class defining its structure (for example, the fields or properties of the class) and methods. Some of these methods can be static functions. The compilation of the program produces a file called an assembly that contains the compiled code in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] intermediate language (MSIL), and a manifest that contains all references to dependent assemblies.  
  
> [!NOTE]  
>  Assemblies are a vital element in the architecture of the CLR. They are the units of packaging, deployment, and versioning of application code in the .NET Framework. Using assemblies, you can deploy application code inside the database and provide a uniform way to administer, back up, and restore complete database applications.  
  
 The assembly manifest contains metadata about the assembly, describing all of the structures, fields, properties, classes, inheritance relationships, functions, and methods defined in the program. The manifest establishes the assembly identity, specifies the files that make up the assembly implementation, specifies the types and resources that make up the assembly, itemizes the compile-time dependencies on other assemblies, and specifies the set of permissions required for the assembly to run properly. This information is used at run time to resolve references, enforce version binding policy, and validate the integrity of loaded assemblies.  
  
 The .NET Framework supports custom attributes for annotating classes, properties, functions, and methods with additional information the application may capture in metadata. All .NET Framework compilers consume these annotations without interpretation and store them as assembly metadata. These annotations can be examined in the same way as any other metadata.  
  
 Managed code is MSIL executed in the CLR, rather than directly by the operating system. Managed code applications acquire CLR services, such as automatic garbage collection, run-time type checking, and security support. These services help provide uniform platform- and language-independent behavior of managed code applications.  
  
## Design Goals of CLR Integration  
 When user code runs inside the CLR-hosted environment in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (called CLR integration), the following design goals apply:  
  
###### Reliability (Safety)  
 User code should not be allowed to perform operations that compromise the integrity of the Database Engine process, such as popping a message box requesting a user response or exiting the process. User code should not be able to overwrite Database Engine memory buffers or internal data structures.  
  
###### Scalability  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the CLR have different internal models for scheduling and memory management. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a cooperative, non-preemptive threading model in which the threads voluntarily yield execution periodically, or when they are waiting on locks or I/O. The CLR supports a preemptive threading model. If user code running inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can directly call the operating system threading primitives, then it does not integrate well into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] task scheduler and can degrade the scalability of the system. The CLR does not distinguish between virtual and physical memory, but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directly manages physical memory and is required to use physical memory within a configurable limit.  
  
 The different models for threading, scheduling, and memory management present an integration challenge for a relational database management system (RDBMS) that scales to support thousands of concurrent user sessions. The architecture should ensure that the scalability of the system is not compromised by user code calling application programming interfaces (APIs) for threading, memory, and synchronization primitives directly.  
  
###### Security  
 User code running in the database must follow [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication and authorization rules when accessing database objects such as tables and columns. In addition, database administrators should be able to control access to operating system resources, such as files and network access, from user code running in the database. This becomes important as managed programming languages (unlike non-managed languages such as Transact-SQL) provide APIs to access such resources. The system must provide a secure way for user code to access machine resources outside the [!INCLUDE[ssDE](../../includes/ssde-md.md)] process. For more information, see [CLR Integration Security](../../relational-databases/clr-integration/security/clr-integration-security.md).  
  
###### Performance  
 Managed user code running in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should have computational performance comparable to the same code run outside the server. Database access from managed user code is not as fast as native [!INCLUDE[tsql](../../includes/tsql-md.md)]. For more information, see [Performance of CLR Integration](../../relational-databases/clr-integration/clr-integration-architecture-performance.md).  
  
## CLR Services  
 The CLR provides a number of services to help achieve the design goals of CLR integration with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
###### Type safety verification  
 Type-safe code is code that accesses memory structures only in well-defined ways. For example, given a valid object reference, type-safe code can access memory at fixed offsets corresponding to actual field members. However, if the code accesses memory at arbitrary offsets inside or outside the range of memory that belongs to the object, then it is not type-safe. When assemblies are loaded in the CLR, prior to the MSIL being compiled using just-in-time (JIT) compilation, the runtime performs a verification phase that examines code to determine its type-safety. Code that successfully passes this verification is called verifiably type-safe code.  
  
###### Application domains  
 The CLR supports the notion of application domains as execution zones within a host process where managed code assemblies can be loaded and executed. The application domain boundary provides isolation between assemblies. The assemblies are isolated in terms of visibility of static variables and data members and the ability to call code dynamically. Application domains are also the mechanism for loading and unloading code. Code can be unloaded from memory only by unloading the application domain. For more information, see [Application Domains and CLR Integration Security](https://msdn.microsoft.com/library/54ee904e-e21a-4ee7-b4ad-a6f6f71bd473).  
  
###### Code Access Security (CAS)  
 The CLR security system provides a way to control what kinds of operations managed code can perform by assigning permissions to code. Code access permissions are assigned based on code identity (for example, the signature of the assembly or the origin of the code).  
  
 The CLR provides for a computer-wide policy that can be set by the computer administrator. This policy defines the permission grants for any managed code running on the machine. In addition, there is a host-level security policy that can be used by hosts such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to specify additional restrictions on managed code.  
  
 If a managed API in the .NET Framework exposes operations on resources that are protected by a code access permission, the API will demand that permission before accessing the resource. This demand causes the CLR security system to trigger a comprehensive check of every unit of code (assembly) in the call stack. Only if the entire call chain has permission will access to the resource be granted.  
  
 Note that the ability to generate managed code dynamically, using the Reflection.Emit API, is not supported inside the CLR-hosted environment in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Such code would not have the CAS permissions to run and would therefore fail at run time. For more information, see [CLR Integration Code Access Security](../../relational-databases/clr-integration/security/clr-integration-code-access-security.md).  
  
###### Host Protection Attributes (HPAs)  
 The CLR provides a mechanism to annotate managed APIs that are part of the .NET Framework with certain attributes that may be of interest to a host of the CLR. Examples of such attributes include:  
  
-   SharedState, which indicates whether the API exposes the ability to create or manage shared state (for example, static class fields).  
  
-   Synchronization, which indicates whether the API exposes the ability to perform synchronization between threads.  
  
-   ExternalProcessMgmt, which indicates whether the API exposes a way to control the host process.  
  
 Given these attributes, the host can specify a list of HPAs, such as the SharedState attribute, that should be disallowed in the hosted environment. In this case, the CLR denies attempts by user code to call APIs that are annotated by the HPAs in the prohibited list. For more information, see [Host Protection Attributes and CLR Integration Programming](../../relational-databases/clr-integration-security-host-protection-attributes/host-protection-attributes-and-clr-integration-programming.md).  
  
## How SQL Server and the CLR Work Together  
 This section discusses how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] integrates the threading, scheduling, synchronization, and memory management models of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the CLR. In particular, this section examines the integration in light of scalability, reliability, and security goals. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] essentially acts as the operating system for the CLR when it is hosted inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The CLR calls low-level routines implemented by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for threading, scheduling, synchronization, and memory management. These are the same primitives that the rest of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine uses. This approach provides several scalability, reliability, and security benefits.  
  
###### Scalability: Common threading, scheduling, and synchronization  
 CLR calls [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] APIs for creating threads, both for running user code and for its own internal use. In order to synchronize between multiple threads, the CLR calls [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] synchronization objects. This allows the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] scheduler to schedule other tasks when a thread is waiting on a synchronization object. For example, when the CLR initiates garbage collection, all of its threads wait for garbage collection to finish. Because the CLR threads and the synchronization objects they are waiting on are known to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] scheduler, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can schedule threads that are running other database tasks not involving the CLR. This also enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to detect deadlocks that involve locks taken by CLR synchronization objects and employ traditional techniques for deadlock removal.  
  
 Managed code runs preemptively in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] scheduler has the ability to detect and stop threads that have not yielded for a significant amount of time. The ability to hook CLR threads to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] threads implies that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] scheduler can identify "runaway" threads in the CLR and manage their priority. Such runaway threads are suspended and put back in the queue. Threads that are repeatedly identified as runaway threads are not allowed to run for a given period of time so that other executing workers can run.  
  
> [!NOTE]  
>  Long-running managed code that accesses data or allocates enough memory to trigger garbage collection will yield automatically. Long-running managed code that does not access data or allocate enough managed memory to trigger garbage collection should explicitly yield by calling the System.Thread.Sleep() function of the .NET Framework.  
  
###### Scalability: Common memory management  
 The CLR calls [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] primitives for allocating and de-allocating its memory. Because the memory used by the CLR is accounted for in the total memory usage of the system, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can stay within its configured memory limits and ensure the CLR and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are not competing with each other for memory. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can also reject CLR memory requests when system memory is constrained, and ask CLR to reduce its memory use when other tasks need memory.  
  
###### Reliability: Application domains and unrecoverable exceptions  
 When managed code in the .NET Framework APIs encounters critical exceptions, such as out-of-memory or stack overflow, it is not always possible to recover from such failures and ensure consistent and correct semantics for their implementation. These APIs raise a thread abort exception in response to these failures.  
  
 When hosted in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such thread aborts are handled as follows: the CLR detects any shared state in the application domain in which the thread abort occurs. The CLR does this by checking for the presence of synchronization objects. If there is shared state in the application domain, then the application domain itself is unloaded. The unloading of the application domain stops database transactions that are currently running in that application domain. Because the presence of shared state can widen the impact of such critical exceptions to user sessions other than the one triggering the exception, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the CLR have taken steps to reduce the likelihood of shared state. For more information, see the .NET Framework documentation.  
  
###### Security: Permission sets  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows users to specify the reliability and security requirements for code deployed into the database. When assemblies are uploaded into the database, the author of the assembly can specify one of three permission sets for that assembly: SAFE, EXTERNAL_ACCESS and UNSAFE.  
  
|||||  
|-|-|-|-|  
|Permission set|SAFE|EXTERNAL_ACCESS|UNSAFE|  
|Code Access Security|Execute only|Execute + access to external resources|Unrestricted|  
|Programming model restrictions|Yes|Yes|No restrictions|  
|Verifiability requirement|Yes|Yes|No|  
|Ability to call native code|No|No|Yes|  
  
 SAFE is the most reliable and secure mode with associated restrictions in terms of the allowed programming model. SAFE assemblies are given enough permission to run, perform computations, and have access to the local database. SAFE assemblies need to be verifiably type safe and are not allowed to call unmanaged code.  
  
 UNSAFE is for highly trusted code that can only be created by database administrators. This trusted code has no code access security restrictions, and it can call unmanaged (native) code.  
  
 EXTERNAL_ACCESS provides an intermediate security option, allowing code to access resources external to the database but still having the reliability guarantees of SAFE.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the host-level CAS policy layer to set up a host policy that grants one of the three sets of permissions based on the permission set stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] catalogs. Managed code running inside the database always gets one of these code access permission sets.  
  
### Programming Model Restrictions  
 The programming model for managed code in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] involves writing functions, procedures, and types which typically do not require the use of state held across multiple invocations or the sharing of state across multiple user sessions. Further, as described earlier, the presence of shared state can cause critical exceptions that impact the scalability and the reliability of the application.  
  
 Given these considerations, we discourage the use of static variables and static data members of classes used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For SAFE and EXTERNAL_ACCESS assemblies, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] examines the metadata of the assembly at CREATE ASSEMBLY time and fails the creation of such assemblies if it finds the use of static data members and variables.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also disallows calls to .NET Framework APIs that are annotated with the **SharedState**, **Synchronization** and **ExternalProcessMgmt** host protection attributes. This prevents SAFE and EXTERNAL_ACCESS assemblies from calling any APIs that enable sharing state, performing synchronization, and affecting the integrity of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. For more information, see [CLR Integration Programming Model Restrictions](../../relational-databases/clr-integration/database-objects/clr-integration-programming-model-restrictions.md).  
  
## See Also  
 [CLR Integration Security](../../relational-databases/clr-integration/security/clr-integration-security.md)   
 [Performance of CLR Integration](../../relational-databases/clr-integration/clr-integration-architecture-performance.md)  
  
  
