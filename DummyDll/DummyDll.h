#pragma once

#ifdef DUMMYDLL_EXPORTS
#define DUMMYDLL_API __declspec(dllexport)
#else
#define DUMMYDLL_API __declspec(dllimport)
#endif // DUMMYDLL_EXPORTS

extern "C" DUMMYDLL_API int __stdcall Helloworld();

