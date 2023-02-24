// Autogenerated with StateSmith
// Autogenerated with StateSmith
// diagram embedded HFileTop
// inside sm - diagram embedded HFileTop
#pragma once
#include <stdint.h>

// any text you put in IRenderConfigC.HFileIncludes (like this comment) will be written to the generated .h file
// diagram embedded HFileIncludes
// inside sm - diagram embedded HFileIncludes
typedef enum Spec2Sm_EventId
{
    Spec2Sm_EventId_DO = 0, // The `do` event is special. State event handlers do not consume this event (ancestors all get it too) unless a transition occurs.
    Spec2Sm_EventId_EV1 = 1,
    Spec2Sm_EventId_EV10 = 2,
    Spec2Sm_EventId_EV2 = 3,
    Spec2Sm_EventId_EV3 = 4,
    Spec2Sm_EventId_EV4 = 5,
    Spec2Sm_EventId_EV5 = 6,
    Spec2Sm_EventId_EV6 = 7,
    Spec2Sm_EventId_EV7 = 8,
    Spec2Sm_EventId_EV8 = 9,
    Spec2Sm_EventId_EV9 = 10,
    Spec2Sm_EventId_EVBACK = 11,
    Spec2Sm_EventId_EVCLOSE = 12,
    Spec2Sm_EventId_EVOPEN = 13,
    Spec2Sm_EventId_EVSTEP = 14,
} Spec2Sm_EventId;

enum
{
    Spec2Sm_EventIdCount = 15
};

typedef enum Spec2Sm_StateId
{
    Spec2Sm_StateId_ROOT = 0,
    Spec2Sm_StateId_AUTO_VAR_TEST = 1,
    Spec2Sm_StateId_AUTO_VAR_TEST__BLAH = 2,
    Spec2Sm_StateId_AUTO_VAR_TEST__BLAH2 = 3,
    Spec2Sm_StateId_DECIDE = 4,
    Spec2Sm_StateId_PREFIXING = 5,
    Spec2Sm_StateId_PREFIXING__ORDER_MENU = 6,
    Spec2Sm_StateId_PREFIXING__OM__BEVERAGE = 7,
    Spec2Sm_StateId_PREFIXING__OM__BEV__NONE = 8,
    Spec2Sm_StateId_PREFIXING__OM__BEV__TEA = 9,
    Spec2Sm_StateId_PREFIXING__OM__BEV__WATER = 10,
    Spec2Sm_StateId_PREFIXING__OM__VEG = 11,
    Spec2Sm_StateId_PREFIXING__OM__VEG__NONE = 12,
    Spec2Sm_StateId_PREFIXING__OM__VEG__POTATO = 13,
    Spec2Sm_StateId_PREFIXING__OM__VEG__YAM = 14,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX = 15,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX__ORDER_MENU = 16,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX__OM_BEVERAGE = 17,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX__OM_BEV_NONE = 18,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX__OM_BEV_TEA = 19,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX__OM_BEV_WATER = 20,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX__OM_VEG = 21,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX__OM_VEG_NONE = 22,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX__OM_VEG_POTATO = 23,
    Spec2Sm_StateId_PREFIXING__SHOWS_MANUAL_PREFIX__OM_VEG_YAM = 24,
    Spec2Sm_StateId_TEST1_DO_EVENT_TESTING = 25,
    Spec2Sm_StateId_TEST1_ROOT = 26,
    Spec2Sm_StateId_TEST1_S1 = 27,
    Spec2Sm_StateId_TEST1_S1_1 = 28,
    Spec2Sm_StateId_TEST1_S2 = 29,
    Spec2Sm_StateId_TEST10_CHOICE_POINT = 30,
    Spec2Sm_StateId_TEST10_A = 31,
    Spec2Sm_StateId_TEST10_A_1 = 32,
    Spec2Sm_StateId_TEST10_ROOT = 33,
    Spec2Sm_StateId_TEST10_G = 34,
    Spec2Sm_StateId_TEST10_G_S0 = 35,
    Spec2Sm_StateId_TEST10_G_S1 = 36,
    Spec2Sm_StateId_TEST10_G_S2 = 37,
    Spec2Sm_StateId_TEST10_G_S3 = 38,
    Spec2Sm_StateId_TEST10_S1 = 39,
    Spec2Sm_StateId_TEST10_S4 = 40,
    Spec2Sm_StateId_TEST2_REGULAR_EVENT_TESTING = 41,
    Spec2Sm_StateId_TEST2_ROOT = 42,
    Spec2Sm_StateId_TEST2_S1 = 43,
    Spec2Sm_StateId_TEST2_S1_1 = 44,
    Spec2Sm_StateId_TEST2_S2 = 45,
    Spec2Sm_StateId_TEST3_BEHAVIOR_ORDERING = 46,
    Spec2Sm_StateId_TEST3_ROOT = 47,
    Spec2Sm_StateId_TEST3_S1 = 48,
    Spec2Sm_StateId_TEST3_S2 = 49,
    Spec2Sm_StateId_TEST3_S3 = 50,
    Spec2Sm_StateId_TEST4_PARENT_CHILD_TRANSITIONS = 51,
    Spec2Sm_StateId_TEST4_B_AND_OTHERS = 52,
    Spec2Sm_StateId_TEST4B_LOCAL = 53,
    Spec2Sm_StateId_TEST4B_G = 54,
    Spec2Sm_StateId_TEST4B_G_1 = 55,
    Spec2Sm_StateId_TEST4C_LOCAL_TO_ALIAS = 56,
    Spec2Sm_StateId_TEST4C_G = 57,
    Spec2Sm_StateId_TEST4C_G_1 = 58,
    Spec2Sm_StateId_TEST4D_EXTERNAL = 59,
    Spec2Sm_StateId_TEST4D_G = 60,
    Spec2Sm_StateId_TEST4D_G_1 = 61,
    Spec2Sm_StateId_TEST4_DECIDE = 62,
    Spec2Sm_StateId_TEST4_ROOT = 63,
    Spec2Sm_StateId_TEST4_S1 = 64,
    Spec2Sm_StateId_TEST4_S10 = 65,
    Spec2Sm_StateId_TEST4_S10_1 = 66,
    Spec2Sm_StateId_TEST4_S2 = 67,
    Spec2Sm_StateId_TEST4_S20 = 68,
    Spec2Sm_StateId_TEST4_S20_1 = 69,
    Spec2Sm_StateId_TEST4_S3 = 70,
    Spec2Sm_StateId_TEST5_PARENT_CHILD_TRANSITIONS_ALIAS = 71,
    Spec2Sm_StateId_TEST5_ROOT = 72,
    Spec2Sm_StateId_TEST5_S1 = 73,
    Spec2Sm_StateId_TEST5_S2 = 74,
    Spec2Sm_StateId_TEST5_S3 = 75,
    Spec2Sm_StateId_TEST6_VARIABLES = 76,
    Spec2Sm_StateId_TEST6_ROOT = 77,
    Spec2Sm_StateId_TEST6_S1 = 78,
    Spec2Sm_StateId_TEST6_S2 = 79,
    Spec2Sm_StateId_TEST7_INITIAL_OR_HISTORY = 80,
    Spec2Sm_StateId_TEST7 = 81,
    Spec2Sm_StateId_T7__DEEP_HISTORY1 = 82,
    Spec2Sm_StateId_T7__DH1__SANTAS_WORKSHOP = 83,
    Spec2Sm_StateId_T7__DH1__ALIENS_DETECTED = 84,
    Spec2Sm_StateId_T7__DH1__GET_BACKUP = 85,
    Spec2Sm_StateId_T7__DH1__HERO = 86,
    Spec2Sm_StateId_T7__DH1__CALL_BATMAN = 87,
    Spec2Sm_StateId_T7__DH1__CALL_THOR = 88,
    Spec2Sm_StateId_T7__DH1__LOCAL_HELP = 89,
    Spec2Sm_StateId_T7__DH1__BUDDY_ELF = 90,
    Spec2Sm_StateId_T7__DH1__POLAR_BEARS = 91,
    Spec2Sm_StateId_T7__DH1__GIVE_COOKIES = 92,
    Spec2Sm_StateId_T7__DH1__SNOWBALL_FIGHT = 93,
    Spec2Sm_StateId_T7__DH1__BUILD = 94,
    Spec2Sm_StateId_T7__DH1__TOOL = 95,
    Spec2Sm_StateId_T7__DH1__CIRCULAR_SAW = 96,
    Spec2Sm_StateId_T7__DH1__IMPACT_DRILL = 97,
    Spec2Sm_StateId_T7__DH1__TOY = 98,
    Spec2Sm_StateId_T7__DH1__GLOW_WORM = 99,
    Spec2Sm_StateId_T7__DH1__RACE_CAR = 100,
    Spec2Sm_StateId_T7__DH1__ROBOT = 101,
    Spec2Sm_StateId_T7__DH1__BATTLEBOT = 102,
    Spec2Sm_StateId_T7__DH1__WALL_E = 103,
    Spec2Sm_StateId_T7__DH1__TEDDY_BEAR = 104,
    Spec2Sm_StateId_T7__DEEP_HISTORY2 = 105,
    Spec2Sm_StateId_T7__DH2__STATE_0 = 106,
    Spec2Sm_StateId_T7__DH2__STATE_1 = 107,
    Spec2Sm_StateId_T7__DH2__STATE_2 = 108,
    Spec2Sm_StateId_T7__DH2__STATE_6 = 109,
    Spec2Sm_StateId_T7__DH2__STATE_9 = 110,
    Spec2Sm_StateId_T7__DH2__STATE_3 = 111,
    Spec2Sm_StateId_T7__DEEP_HISTORY3 = 112,
    Spec2Sm_StateId_T7__DH3__STATE_0 = 113,
    Spec2Sm_StateId_T7__DH3__STATE_1 = 114,
    Spec2Sm_StateId_T7__DH3__STATE_2 = 115,
    Spec2Sm_StateId_T7__DH3__STATE_6 = 116,
    Spec2Sm_StateId_T7__DH3__STATE_9 = 117,
    Spec2Sm_StateId_T7__DH3__STATE_3 = 118,
    Spec2Sm_StateId_T7__HISTORY1 = 119,
    Spec2Sm_StateId_T7__H1__OFF = 120,
    Spec2Sm_StateId_T7__H1__OFF1 = 121,
    Spec2Sm_StateId_T7__H1__OFF2 = 122,
    Spec2Sm_StateId_T7__H1__OFF3 = 123,
    Spec2Sm_StateId_T7__H1__ON = 124,
    Spec2Sm_StateId_T7__H1__ON1 = 125,
    Spec2Sm_StateId_T7__H1__ON2 = 126,
    Spec2Sm_StateId_T7__H1__ON3 = 127,
    Spec2Sm_StateId_T7__INITIAL1 = 128,
    Spec2Sm_StateId_T7__INITIAL1__PARENT = 129,
    Spec2Sm_StateId_T7__INITIAL1__G = 130,
    Spec2Sm_StateId_T7__INITIAL1__G_S1 = 131,
    Spec2Sm_StateId_T7__INITIAL1__G_S2 = 132,
    Spec2Sm_StateId_T7__INITIAL1__G_S3 = 133,
    Spec2Sm_StateId_T7__INITIAL1__S1 = 134,
    Spec2Sm_StateId_TEST7_DECIDE = 135,
    Spec2Sm_StateId_TEST8_ENTRY_CHOICE = 136,
    Spec2Sm_StateId_TEST8_ROOT = 137,
    Spec2Sm_StateId_TEST8_G = 138,
    Spec2Sm_StateId_TEST8_G_S1 = 139,
    Spec2Sm_StateId_TEST8_G_S2 = 140,
    Spec2Sm_StateId_TEST8_G_S3 = 141,
    Spec2Sm_StateId_TEST8_S1 = 142,
    Spec2Sm_StateId_TEST9_EXIT_CHOICE = 143,
    Spec2Sm_StateId_TEST9_DECIDE = 144,
    Spec2Sm_StateId_TEST9_ROOT = 145,
    Spec2Sm_StateId_TEST9_G_S1 = 146,
    Spec2Sm_StateId_TEST9_G_S2 = 147,
    Spec2Sm_StateId_TEST9_G_S3 = 148,
    Spec2Sm_StateId_TEST9_G_S4 = 149,
    Spec2Sm_StateId_TEST9_S1 = 150,
    Spec2Sm_StateId_TEST9_S1_1 = 151,
    Spec2Sm_StateId_TEST9A_ROOT = 152,
    Spec2Sm_StateId_TEST9A_S1 = 153,
    Spec2Sm_StateId_TEST9A_S1_1 = 154,
} Spec2Sm_StateId;

enum
{
    Spec2Sm_StateIdCount = 155
};

typedef enum Spec2Sm_T7__H1__ON_HistoryId
{
    Spec2Sm_T7__H1__ON_HistoryId_T7__H1__ON1 = 0, // default transition
    Spec2Sm_T7__H1__ON_HistoryId_T7__H1__ON2 = 1,
    Spec2Sm_T7__H1__ON_HistoryId_T7__H1__ON3 = 2,
} Spec2Sm_T7__H1__ON_HistoryId;


typedef enum Spec2Sm_T7__H1__OFF_HistoryId
{
    Spec2Sm_T7__H1__OFF_HistoryId_T7__H1__OFF1 = 0, // default transition
    Spec2Sm_T7__H1__OFF_HistoryId_T7__H1__OFF2 = 1,
    Spec2Sm_T7__H1__OFF_HistoryId_T7__H1__OFF3 = 2,
} Spec2Sm_T7__H1__OFF_HistoryId;


typedef enum Spec2Sm_T7__DH1__ALIENS_DETECTED_HistoryId
{
    Spec2Sm_T7__DH1__ALIENS_DETECTED_HistoryId_T7__DH1__SNOWBALL_FIGHT = 0, // default transition
    Spec2Sm_T7__DH1__ALIENS_DETECTED_HistoryId_T7__DH1__GIVE_COOKIES = 1,
    Spec2Sm_T7__DH1__ALIENS_DETECTED_HistoryId_T7__DH1__HERO = 2,
    Spec2Sm_T7__DH1__ALIENS_DETECTED_HistoryId_T7__DH1__BUDDY_ELF = 3,
    Spec2Sm_T7__DH1__ALIENS_DETECTED_HistoryId_T7__DH1__POLAR_BEARS = 4,
} Spec2Sm_T7__DH1__ALIENS_DETECTED_HistoryId;


typedef enum Spec2Sm_T7__DH1__GET_BACKUP_HistoryId
{
    Spec2Sm_T7__DH1__GET_BACKUP_HistoryId_T7__DH1__GET_BACKUPChoicePoint = 0, // default transition
    Spec2Sm_T7__DH1__GET_BACKUP_HistoryId_T7__DH1__HERO = 1,
    Spec2Sm_T7__DH1__GET_BACKUP_HistoryId_T7__DH1__BUDDY_ELF = 2,
    Spec2Sm_T7__DH1__GET_BACKUP_HistoryId_T7__DH1__POLAR_BEARS = 3,
} Spec2Sm_T7__DH1__GET_BACKUP_HistoryId;


typedef enum Spec2Sm_T7__DH1__BUILD_HistoryId
{
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__TOY = 0, // default transition
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__TOOL = 1,
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__RACE_CAR = 2,
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__TEDDY_BEAR = 3,
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__GLOW_WORM = 4,
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__ROBOT = 5,
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__BATTLEBOT = 6,
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__WALL_E = 7,
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__IMPACT_DRILL = 8,
    Spec2Sm_T7__DH1__BUILD_HistoryId_T7__DH1__CIRCULAR_SAW = 9,
} Spec2Sm_T7__DH1__BUILD_HistoryId;


typedef enum Spec2Sm_T7__DH2__state_0_HistoryId
{
    Spec2Sm_T7__DH2__state_0_HistoryId_T7__DH2__state_1 = 0, // default transition
    Spec2Sm_T7__DH2__state_0_HistoryId_T7__DH2__state_2 = 1,
    Spec2Sm_T7__DH2__state_0_HistoryId_T7__DH2__state_6 = 2,
    Spec2Sm_T7__DH2__state_0_HistoryId_T7__DH2__state_9 = 3,
} Spec2Sm_T7__DH2__state_0_HistoryId;


typedef enum Spec2Sm_T7__DH3__state_0_HistoryId
{
    Spec2Sm_T7__DH3__state_0_HistoryId_T7__DH3__state_1 = 0, // default transition
    Spec2Sm_T7__DH3__state_0_HistoryId_T7__DH3__state_2 = 1,
} Spec2Sm_T7__DH3__state_0_HistoryId;


// Generated state machine
// forward declaration
typedef struct Spec2Sm Spec2Sm;

// State machine variables. Can be used for inputs, outputs, user variables...
typedef struct Spec2Sm_Vars
{
    Spec2Sm_T7__H1__ON_HistoryId T7__H1__ON_history;
    Spec2Sm_T7__H1__OFF_HistoryId T7__H1__OFF_history;
    Spec2Sm_T7__DH1__ALIENS_DETECTED_HistoryId T7__DH1__ALIENS_DETECTED_history;
    Spec2Sm_T7__DH1__GET_BACKUP_HistoryId T7__DH1__GET_BACKUP_history;
    Spec2Sm_T7__DH1__BUILD_HistoryId T7__DH1__BUILD_history;
    Spec2Sm_T7__DH2__state_0_HistoryId T7__DH2__state_0_history;
    Spec2Sm_T7__DH3__state_0_HistoryId T7__DH3__state_0_history;
    uint8_t count;
    uint8_t auto_var_1;
} Spec2Sm_Vars;



// event handler type
typedef void (*Spec2Sm_Func)(Spec2Sm* self);

// State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
void Spec2Sm_ctor(Spec2Sm* self);

// Starts the state machine. Must be called before dispatching events. Not thread safe.
void Spec2Sm_start(Spec2Sm* self);

// Dispatches an event to the state machine. Not thread safe.
void Spec2Sm_dispatch_event(Spec2Sm* self, Spec2Sm_EventId event_id);

// Generated state machine
struct Spec2Sm
{
    // Used internally by state machine. Feel free to inspect, but don't modify.
    Spec2Sm_StateId state_id;
    
    // Used internally by state machine. Don't modify.
    Spec2Sm_Func ancestor_event_handler;
    
    // Used internally by state machine. Don't modify.
    Spec2Sm_Func current_event_handlers[Spec2Sm_EventIdCount];
    
    // Used internally by state machine. Don't modify.
    Spec2Sm_Func current_state_exit_handler;
    
    // Variables. Can be used for inputs, outputs, user variables...
    Spec2Sm_Vars vars;
};

