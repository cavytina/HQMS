#  国家绩效考核管理系统 V0.0.1  
  本管理系统前端程序采用WPF结合MaterialDesign、MaterialDesignExtensions控件完成，系统本地设置采用SQLite提供持久化支持。    

## 程序环境  
|软件环境         |   版本 | 
| :----          |  :---- |
| Visual Studio Enterprise 2019 | 16.9.4 |
| .NET FrameWork | 4.7.2 |
| Prism.Wpf      | 8.1.97 |
| MaterialDesignThemes | 4.5.0 |
| MaterialDesignColors | 2.0.6 |
| MaterialDesignExtensions | 3.3.0 |
| Microsoft.Practices.EnterpriseLibrary | 6.0.0.0 |
| SQLite.Core    | 1.0.116.0 |
| Dapper         | 2.0.123 |
| Newtonsoft.Json | 13.0.1 |

TODO
- [ ] Microsoft.Practices.EnterpriseLibrary.LoggingBlock => Log4Net  
- [ ] Microsoft.Practices.EnterpriseLibrary.ValidationBlock => FluentValidation  
- [ ] Microsoft.Practices.EnterpriseLibrary.TransientFaultHandlingBlock => Polly  
- [ ] System.Runtiem.caching  

## MaterialDesign控件风格约定
| 控件类型  | 默认风格名称 | 是否需要指定默认风格 |
| :----          |  :---- |  :---- |
| Window | MaterialDesignWindow | 是 |
| TextBlock | MaterialDesignTextBlock | 是 |
| ComboBox | MaterialDesignComboBox | 否 |
| ListBox | MaterialDesignChoiceChipPrimaryOutlineListBox | 是 |
| Button | MaterialDesignOutlinedLightButton | 是 |

##  程序设计规范  
### 1. 程序设计原则
- 各层之间交互关系如下:视图=>视图模型=>模型=>服务，视图模型层不直接使用服务层方法获取数据，模型层  
  负责将服务层方法获取的数据进行初步加工并交由视图模型层调用。 
- 对于程序错误只在程序中(即为Snackbar)提示出现错误，错误详细信息则记录在日志文件中，即视图模型层调用  
  SnackbarMessageQueue完成错误提示，服务层调用LogService完成日志记录。   
- 服务层实现类都暴露公共的返回值为布尔类型的方法，方法参数中可以指定是否需要返回外部类型参数(使用out关键字).   
- 核心层提供全局单例服务接口IEnvironmentMonitor供外部调用，包含系统默认路径设置、系统默认日志、系统数据库设置。   
- 服务层与应用层通讯基于Prism.PubSubEvent\<string>实现，通讯报文格式采用json格式。  

### 2. 命名规范  
1.字段、变量采用小驼峰法，属性、类名、方法名采用大驼峰法。  
2.只含有属性定义的简单类型，以Kind结尾。  
3.枚举类型以Part结尾。  
4.List\<T>类型以Hub结尾。  
5.Dictionary\<T>类型以Shub结尾。  
6.ICollection\<T>类型以Collecter结尾。  

### 3. 缩略语规范  

###  4. 服务层通讯协议
#### 4.1 服务请求报文格式定义
| 序号 |   标识   |    名称      |   类型 | 长度 | 字典标识 |
| :----|  :----  |  :----       | :----  |:---- |:---- |
|  1  | svc_code | 请求服务代码  | 字符型 |  2  |         |
|  2  | svc_name | 请求服务名称  | 字符型 |  10 |         |
|  3  | svc_desc | 请求服务描述  | 字符型 |  50 |         |
|  4  | request_module_name |    请求模块名称   | 字符型 |  50 |         |
|  5  | svc_cont |  请求内容     | 字符型 |  8000 |         |

#### 4.2 服务应答报文格式定义
| 序号 |   标识   |    名称      |   类型 | 长度 | 字典标识 |
| :----|  :----  |  :----       | :----  |:---- |:---- |
|  1  | svc_code |  应答服务代码 | 字符型 |  2  |         |
|  2  | svc_name |  应答服务名称 | 字符型 |  10 |         |
|  3  | svc_desc |  应答服务描述 | 字符型 |  50 |         |
|  4  | response_module_name |  应答模块名称  | 字符型 |  50 |         |
|  5  | ret_code |  应答返回代码 | 字符型 |  2  |         |
|  6  | ret_msg  |  应答错误信息 | 字符型 | 500 |         |
|  7  | svc_cont |  应答内容   | 字符型 |  8000 |         |

#### 4.1.1 账户认证服务请求内容报文格式定义  (AccountAuthenticationService)
| 序号 |   标识   |    名称      |   类型 | 长度 | 字典标识 |
| :----|  :----  |  :----       | :----  |:---- |:---- |
|  1  | account  | 账户代码  | 字符型 |  10  |         |
|  2  | password | 账户密码  | 字符型 |  10 |          |

#### 4.2.1 账户认证服务应答内容报文格式定义 
| 序号 |   标识   |    名称      |   类型 | 长度 | 字典标识 |
| :----|  :----  |  :----       | :----  |:---- |:---- |
|  1  | name     |  账户名称  | 字符型 |  50  |         |     |

#### 4.1.2 窗口状态请求内容报文格式定义  (WindowStatusService)
| 序号 |   标识   |    名称      |   类型 | 长度 | 字典标识 |
| :----|  :----  |  :----       | :----  |:---- |:---- |
|  1  | account  | 账户代码  | 字符型 |  10  |         |
|  2  | password | 账户密码  | 字符型 |  10 |          |