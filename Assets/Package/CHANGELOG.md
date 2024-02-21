## 1.1.0 [21.02.2024]

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