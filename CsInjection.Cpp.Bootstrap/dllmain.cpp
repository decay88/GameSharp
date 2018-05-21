// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include <fstream>
#include <metahost.h>
#pragma comment(lib, "mscoree.lib")

bool fexists(const wchar_t *filename)
{
	std::ifstream ifile(filename);
	return (bool)ifile;
}

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

	auto dll = L"D:\\Repos\\CsInjection\\Output\\CsInjection.LeagueOfLegends.dll";
	if (!fexists(dll))
	{
		throw 1;
	}
	pRuntimeHost->ExecuteInDefaultAppDomain(dll, L"CsInjection.LeagueOfLegends.Engine", L"Initialize", L"", &dwRet);
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

