---
title: "ADO Event Instantiation: Visual C++"
description: "ADO Event Instantiation: Visual C++"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
dev_langs:
  - "C++"
---
# ADO Event Instantiation: Visual C++
This is a schematic description of how to instantiate ADO events in Microsoft® Visual C++®. See [ADO Events Model Example (VC++)](../../reference/ado-api/ado-events-model-example-vc.md) for a complete description.  
  
 Create classes derived from the **ConnectionEventsVt** and **RecordsetEventsVt** interfaces found in the file adoint.h.  
  
```  
// BeginEventExampleVC01  
class CConnEvent : public ConnectionEventsVt  
{  
    public:  
    STDMETHODIMP InfoMessage(   
            ADOError *pError,  
            EventStatusEnum *adStatus,  
            _ADOConnection *pConnection);  
...  
}  
  
class CRstEvent : public RecordsetEventsVt   
{  
    public:  
        STDMETHODIMP WillChangeField(   
                LONG cFields,  
                VARIANT Fields,  
                EventStatusEnum *adStatus,  
                _ADORecordset *pRecordset);  
...  
}  
// EndEventExampleVC01  
```  
  
 Implement each of the event-handler methods in both classes. It is sufficient that each method merely return an HRESULT of S_OK. However, when you make it known that your event handlers are available, they will be called continuously by default. Instead, you might want to request no further notification after the first time by setting **adStatus** to **adStatusUnwantedEvent**.  
  
```  
// BeginEventExampleVC02  
STDMETHODIMP CConnEvent::ConnectComplete(  
            ADOError *pError,  
            EventStatusEnum *adStatus,  
            _ADOConnection *pConnection)   
        {  
        *adStatus = adStatusUnwantedEvent;  
        return S_OK;  
        }  
  
// EndEventExampleVC02  
```  
  
 The event classes inherit from **IUnknown**, so you must also implement the **QueryInterface**, **AddRef**, and **Release** methods. Also implement class constructors and destructors. Choose the Visual C++ tools with which you are most comfortable to simplify this part of the task.  
  
 Make it known that your event handlers are available by issuing **QueryInterface** on the [Recordset](../../reference/ado-api/recordset-object-ado.md) and [Connection](../../reference/ado-api/connection-object-ado.md) objects for the **IConnectionPointContainer** and **IConnectionPoint** interfaces. Then issue **IConnectionPoint::Advise** for each class.  
  
 For example, assume you are using a Boolean function that returns **True** if it successfully informs a **Recordset** object that you have event handlers available.  
  
```  
// BeginEventExampleVC03  
HRESULT    hr;  
DWORD      dwEvtClass;  
IConnectionPointContainer    *pCPC = NULL;  
IConnectionPoint             *pCP = NULL;  
CRstEvent                    *pRStEvent = NULL;  
...  
_RecordsetPtr                pRs;  
pRs.CreateInstance(__uuidof(Recordset));  
pRStEvent = new CRstEvent;  
if (pRStEvent == NULL) return FALSE;  
...  
hr = pRs->QueryInterface(IID_IConnectionPointContainer, (LPVOID *)&pCPC);  
if (FAILED(hr)) return FALSE;  
hr = pCPC->FindConnectionPoint(RecordsetEvents, &pCP);  
pCPC->Release();    // Always Release now, even before checking.  
if (FAILED(hr)) return FALSE;  
hr = pCP->Advise(pRstEvent, &dwEvtClass);   //Turn on event support.  
pCP->Release();  
if (FAILED(hr)) return FALSE;  
...  
return TRUE;  
...  
// EndEventExampleVC03  
```  
  
 At this point, events for the **RecordsetEvent** family are enabled and your methods will be called as **Recordset** events occur.  
  
 Later, when you want to make your event handlers unavailable, get the connection point again and issue the **IConnectionPoint::Unadvise** method.  
  
```  
// BeginEventExampleVC04  
...  
hr = pCP->Unadvise(dwEvtClass);    //Turn off event support.  
pCP->Release();  
if (FAILED(hr)) return FALSE;  
...  
// EndEventExampleVC04  
```  
  
 You must release interfaces and destroy class objects as appropriate.  
  
 The following code shows a complete example of a **Recordset** Event sink class.  
  
```  
// BeginEventExampleVC05.cpp  
// compile with: /LD  
#include <adoint.h>  
  
class CADORecordsetEvents : public RecordsetEventsVt {  
  
public:  
   ULONG m_ulRefCount;  
   CADORecordsetEvents():m_ulRefCount(1){}  
  
   STDMETHOD(QueryInterface)(REFIID iid, LPVOID * ppvObject) {  
      if (IsEqualIID(__uuidof(IUnknown), iid) || IsEqualIID(__uuidof(RecordsetEventsVt), iid)) {  
         *ppvObject = this;  
         return S_OK;  
      }  
      else   
         return E_NOINTERFACE;  
   }  
  
   STDMETHOD_(ULONG, AddRef)() {  
      return m_ulRefCount++;  
   }  
  
   STDMETHOD_(ULONG, Release)() {  
      if (--m_ulRefCount == 0) {  
         delete this;  
         return 0;  
      }  
      else   
         return m_ulRefCount;  
   }  
  
   STDMETHOD(WillChangeField)( LONG cFields,   
                               VARIANT Fields,   
                               EventStatusEnum *adStatus,  
                               _ADORecordset *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(FieldChangeComplete)( LONG cFields,  
                                   VARIANT Fields,  
                                   ADOError *pError,  
                                   EventStatusEnum *adStatus,  
                                   _ADORecordset *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(WillChangeRecord)( EventReasonEnum adReason,  
                                LONG cRecords,  
                                EventStatusEnum *adStatus,  
                                _ADORecordset *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(RecordChangeComplete)( EventReasonEnum adReason,  
                                    LONG cRecords,  
                                    ADOError  *pError,  
                                    EventStatusEnum  *adStatus,  
                                    _ADORecordset  *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(WillChangeRecordset)( EventReasonEnum adReason,  
                                   EventStatusEnum *adStatus,  
                                   _ADORecordset  *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(RecordsetChangeComplete)( EventReasonEnum adReason,  
                                       ADOError *pError,  
                                       EventStatusEnum  *adStatus,  
                                       _ADORecordset  *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(WillMove)( EventReasonEnum adReason,  
                        EventStatusEnum  *adStatus,  
                        _ADORecordset  *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(MoveComplete)( EventReasonEnum adReason,  
                            ADOError *pError,  
                            EventStatusEnum *adStatus,  
                            _ADORecordset  *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(EndOfRecordset)( VARIANT_BOOL *fMoreData,  
                              EventStatusEnum *adStatus,  
                              _ADORecordset *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(FetchProgress)( long Progress,  
                             long MaxProgress,  
                             EventStatusEnum *adStatus,  
                             _ADORecordset *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
  
   STDMETHOD(FetchComplete)( ADOError *pError,  
                             EventStatusEnum *adStatus,  
                             _ADORecordset *pRecordset) {  
      *adStatus = adStatusUnwantedEvent;   
      return S_OK;  
   }  
};  
```