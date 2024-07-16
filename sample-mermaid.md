```mermaid
graph TD;
    Trigger --> EventA --> Action1
                EventA --> Action2
    
    Action1 --> EventB
    Action2 --> EventC --> Action3 --> EventD
```