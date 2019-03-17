---
title: "Use SQL Server Extended Events (XEvents) to Monitor Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: b57cc2fe-52dc-4fa9-8554-5a866e25c6d7
author: minewiskan
ms.author: owend
manager: craigg
---
# Use SQL Server Extended Events (XEvents) to Monitor Analysis Services
  Analysis Services provides tracing capabilities through the usage of [Extended Events](../../relational-databases/extended-events/extended-events.md).  
  
 Extended Events is an event infrastructure that is highly scalable and configurable for server systems. Extended Events is a light weight performance monitoring system that uses very few performance resources.  
  
 All Analysis Services events can be captured and target to specific consumers, as defined in [Extended Events](../../relational-databases/extended-events/extended-events.md), through XEvents.  
  
## Initiating Extended Events in Analysis Services  
 Extended Event tracing is enabled using a similar XMLA create object script command as shown below:  
  
```  
<Execute ...>  
   <Command>  
      <Batch ...>  
         <Create ...>  
            <ObjectDefinition>  
               <Trace>  
                  <ID>trace_id</ID>  
                  <Name>trace_name</Name>  
                  <ddl300_300:XEvent>  
                     <event_session ...>  
                        <event package="AS" name="AS_event">  
                           <action package="PACKAGE0" .../>  
                        </event>  
                        <target package="PACKAGE0" name="asynchronous_file_target">  
                           <parameter name="filename" value="data_filename.xel"/>  
                           <parameter name="metadatafile" value="metadata_filename.xem"/>  
                        </target>  
                     </event_session>  
                  </ddl300_300:XEvent>  
               </Trace>  
            </ObjectDefinition>  
         </Create>  
      </Batch>  
   </Command>  
   <Properties></Properties>  
</Execute>  
  
```  
  
 Where the following elements are to be defined by the user, depending on the tracing needs:  
  
 *trace_id*  
 Defines the unique identifier for this trace.  
  
 *trace_name*  
 The name given to this trace; usually a human readable definition of the trace. It is a common practice to use the *trace_id* value as the name.  
  
 *AS_event*  
 The Analysis Services event to be exposed. See [Analysis Services Trace Events](https://docs.microsoft.com/bi-reference/trace-events/analysis-services-trace-events) for names of the events.  
  
 *data_filename*  
 The name of the file that contains the events data. This name is suffixed with a time stamp to avoid data overwriting if the trace is sent over and over.  
  
 *metadata_filename*  
 The name of the file that contains the events metadata. This name is suffixed with a time stamp to avoid data overwriting if the trace is sent over and over.  
  
## Stopping Extended Events in Analysis Services  
 To stop the Extended Events tracing object you need to delete that object using a similar XMLA delete object script command as shown below:  
  
```  
<Execute xmlns="urn:schemas-microsoft-com:xml-analysis">  
   <Command>  
      <Batch ...>  
         <Delete ...>  
            <Object>  
               <TraceID>trace_id</TraceID>  
            </Object>  
         </Delete>  
      </Batch>  
   </Command>  
   <Properties></Properties>  
</Execute>  
  
```  
  
 Where the following elements are to be defined by the user, depending on the tracing needs:  
  
 *trace_id*  
 Defines the unique identifier for the trace to be deleted.  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
