// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include <metahost.h>
#pragma comment(lib, "mscoree.lib")

void SetupRuntime()
{
	ICLRMetaHost    *pMetaHost = nullptr;
	ICLRRuntimeHost *pRuntimeHost = nullptr;
	ICLRRuntimeInfo *pRuntimeInfo = nullptr;
	DWORD dwRet = 0;

	CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, (LPVOID*)&pMetaHost);
	pMetaHost->GetRuntime(L"v4.0.30319", IID_PPV_ARGS(&pRuntimeInfo));
	pRuntimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_PPV_ARGS(&pRuntimeHost));
	pRuntimeHost->Start();

	pRuntimeHost->ExecuteInDefaultAppDomain(L"C:\\Users\\sander\\source\\repos\\CsInjection\\Output\\CsInjection.LeagueOfLegends.dll", 
		L"CsInjection.LeagueOfLegends.Engine", L"Initialize", L"", &dwRet);
	pRuntimeHost->Stop();

	if (pRuntimeInfo != nullptr)
	{
		pRuntimeInfo->Release();
		pRuntimeInfo = nullptr;
	}
	if (pRuntimeHost != nullptr)
	{
		pRuntimeHost->Release();
		pRuntimeHost = nullptr;
	}
	if (pMetaHost != nullptr)
	{
		pMetaHost->Release();
		pMetaHost = nullptr;
	}
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
		SetupRuntime();
	case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

