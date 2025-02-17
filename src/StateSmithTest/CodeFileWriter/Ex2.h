// Autogenerated with StateSmith 0.8.14-alpha.
// Algorithm: Balanced1. See https://github.com/StateSmith/StateSmith/wiki/Algorithms

#pragma once
#include <stdint.h>

typedef enum Ex2_EventId
{
    Ex2_EventId_DO = 0, // The `do` event is special. State event handlers do not consume this event (ancestors all get it too) unless a transition occurs.
    Ex2_EventId_EV2 = 1,
    Ex2_EventId_MYEV1 = 2,
} Ex2_EventId;

enum
{
    Ex2_EventIdCount = 3
};

typedef enum Ex2_StateId
{
    Ex2_StateId_ROOT = 0,
    Ex2_StateId_STATE_1 = 1,
    Ex2_StateId_STATE_2 = 2,
} Ex2_StateId;

enum
{
    Ex2_StateIdCount = 3
};


// Generated state machine
// forward declaration
typedef struct Ex2 Ex2;

// event handler type
typedef void (*Ex2_Func)(Ex2* sm);

// State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
void Ex2_ctor(Ex2* sm);

// Starts the state machine. Must be called before dispatching events. Not thread safe.
void Ex2_start(Ex2* sm);

// Dispatches an event to the state machine. Not thread safe.
void Ex2_dispatch_event(Ex2* sm, Ex2_EventId event_id);

// Thread safe.
char const * Ex2_state_id_to_string(Ex2_StateId id);

// Thread safe.
char const * Ex2_event_id_to_string(Ex2_EventId id);

// Generated state machine
struct Ex2
{
    // Used internally by state machine. Feel free to inspect, but don't modify.
    Ex2_StateId state_id;
    
    // Used internally by state machine. Don't modify.
    Ex2_Func ancestor_event_handler;
    
    // Used internally by state machine. Don't modify.
    Ex2_Func current_event_handlers[Ex2_EventIdCount];
    
    // Used internally by state machine. Don't modify.
    Ex2_Func current_state_exit_handler;
};

// Converts an event id to a string. Thread safe.
const char* Ex2_event_id_to_string(const enum EventId id);
