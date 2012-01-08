
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;"
ASSEMBLY = bin/Debug/SDL.net.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

SDL_NET_DLL_MDB_SOURCE=bin/Debug/SDL.net.dll.mdb
SDL_NET_DLL_MDB=$(BUILD_DIR)/SDL.net.dll.mdb

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/SDL.net.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

SDL_NET_DLL_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(SDL_NET_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(SDL_PC)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	AssemblyInfo.cs \
	Core/BooleanType.cs \
	Core/EventMask.cs \
	Core/Events.cs \
	Core/EventTypes.cs \
	Core/NativeFunctionReturnFlags.cs \
	Core/QuitEventArgs.cs \
	Core/ResourceBase.cs \
	Core/SdlEventArgs.cs \
	Core/SdlException.cs \
	Core/SdlSystem.cs \
	Core/Timer.cs \
	Core/UserEventArgs.cs \
	Input/KeyboardEventArgs.cs \
	Core/ActiveEventArgs.cs \
	Input/MouseMotionEventArgs.cs \
	Input/MouseButtonEventArgs.cs \
	Input/JoystickButtonEventArgs.cs \
	Input/JoystickAxisEventArgs.cs \
	Input/JoystickHatEventArgs.cs \
	Input/JoystickBallEventArgs.cs \
	Graphics/VideoResizeEventArgs.cs \
	Graphics/VideoExposeEventArgs.cs \
	Core/TickEventArgs.cs \
	Audio/ChannelFinishedEventArgs.cs \
	Audio/MusicFinishedEventArgs.cs \
	Core/WindowManagerEventArgs.cs \
	Audio/SdlMixerSystem.cs \
	Audio/Mixer.cs \
	Graphics/SurfaceLostException.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	Audio \
	Graphics \
	Input \
	sdl.pc.in 

REFERENCES =  \
	System

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

include $(top_srcdir)/Makefile.include

SDL_PC = $(BUILD_DIR)/sdl.pc

$(eval $(call emit-deploy-wrapper,SDL_PC,sdl.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
