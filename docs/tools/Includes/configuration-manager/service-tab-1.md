---
author: cwebb-msft
ms.author: chweb
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: include
ms.collection:
  - data-tools
---
#### Binary Path

Displays the location of the program files used by this service.

#### Error Control

`1` indicates `SERVICE_ERROR_NORMAL`. If the service fails to start during computer startup, the startup program logs the error and displays a pop-up message box but continues the startup operation. This value can't be changed.

#### Exit Code

When an error occurs, the error number appears in this box.

The Windows error code defines any problems encountered in starting or stopping the service. This property is set to `ERROR_SERVICE_SPECIFIC_ERROR` (`1066`) when the error is unique to the service represented by this class, and information about the error is available in the `ServiceSpecificExitCode` property. The service sets this value to `NO_ERROR` (`0`) when running, and again upon normal termination.

Use this number to troubleshoot failures by searching on Microsoft Learn, or provide the number to your technical support staff.
