// Autogenerated with StateSmith 0.8.14-alpha.
// Algorithm: Balanced1. See https://github.com/StateSmith/StateSmith/wiki/Algorithms

#pragma once
#include <stdint.h>

// any text you put in IRenderConfigC.HFileIncludes (like this comment) will be written to the generated .h file
typedef enum __attribute__((packed)) ButtonSm1_EventId
{
    ButtonSm1_EventId_DO = 0, // The `do` event is special. State event handlers do not consume this event (ancestors all get it too) unless a transition occurs.
} ButtonSm1_EventId;

enum
{
    ButtonSm1_EventIdCount = 1
};

typedef enum __attribute__((packed)) ButtonSm1_StateId
{
    ButtonSm1_StateId_ROOT = 0,
    ButtonSm1_StateId_NOT_PRESSED = 1,
    ButtonSm1_StateId_PRESSED = 2,
    ButtonSm1_StateId_CONFIRMING_HELD = 3,
    ButtonSm1_StateId_HELD = 4,
} ButtonSm1_StateId;

enum
{
    ButtonSm1_StateIdCount = 5
};


// Generated state machine
// forward declaration
typedef struct ButtonSm1 ButtonSm1;

// State machine variables. Can be used for inputs, outputs, user variables...
typedef struct ButtonSm1_Vars
{
    // Note! This example below uses bitfields just to show that you can. They aren't required and might not
    // save you any actual RAM depending on the compiler struct padding/alignment/enum size... One day, we will be able choose where the vars
    // structure is positioned relative to the other state machine fields.
    // You can convert any of the fields below from bitfields and the code will still work fine.
    
    /** used by state machine. If you change bitfield size, also update `time_ms` expansion masking. */
    uint16_t debounce_started_at_ms : 11;
    
    uint16_t input_is_pressed : 1; // input
    uint16_t output_event_press : 1; // output
    uint16_t output_event_release : 1; // output
    uint16_t output_event_held : 1; // output
    uint16_t output_event_tap : 1; // output
} ButtonSm1_Vars;


// event handler type
typedef void (*ButtonSm1_Func)(ButtonSm1* sm);

// State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
void ButtonSm1_ctor(ButtonSm1* sm);

// Starts the state machine. Must be called before dispatching events. Not thread safe.
void ButtonSm1_start(ButtonSm1* sm);

// Dispatches an event to the state machine. Not thread safe.
void ButtonSm1_dispatch_event(ButtonSm1* sm, ButtonSm1_EventId event_id);

// Thread safe.
char const * ButtonSm1_state_id_to_string(ButtonSm1_StateId id);

// Thread safe.
char const * ButtonSm1_event_id_to_string(ButtonSm1_EventId id);

// Generated state machine
struct ButtonSm1
{
    // Used internally by state machine. Feel free to inspect, but don't modify.
    ButtonSm1_StateId state_id;
    
    // Used internally by state machine. Don't modify.
    ButtonSm1_Func ancestor_event_handler;
    
    // Used internally by state machine. Don't modify.
    ButtonSm1_Func current_event_handlers[ButtonSm1_EventIdCount];
    
    // Used internally by state machine. Don't modify.
    ButtonSm1_Func current_state_exit_handler;
    
    // Variables. Can be used for inputs, outputs, user variables...
    ButtonSm1_Vars vars;
};

