## 1.1.2 [29.02.2024]

### Added:
- Possibility to override OngoingState property in the BaseStateMachine class

### Changed:
- Fix searching for starting state in the State Machine

## 1.1.1 [21.02.2024]

### Changed:
- Fix attempts to re-enter current state

## 1.1.0 [20.02.2024]

### Added:
- 'Any' state feature for the State Machine implementation

### Changed:
- Fix disposing issues in the ObjectPool implementation
- StateMachine implementation now utilizes interface (IState) instead of the BaseState class

## 1.0.2 [30.09.2023]

### Added:
- ManagerBase - base class that implements IInitializable and IDeinitializable interfaces
- Basic Vector3 extensions
- Basic string extensions

## 1.0.1 [19.02.2023]

### Added:
- IDeinitializable interface
- OnInitialized event to IInitializable & IInitializableWithArgument interfaces

### Changed:
- IInitializableWithArgument is no longer inheriting from the IInitializable interface