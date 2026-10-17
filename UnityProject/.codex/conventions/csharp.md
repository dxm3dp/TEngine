# C# 编码规范

## 适用范围

- 本规范适用于 UnityProject 中由本项目自主维护的 C# 代码，仅对本次任务新增或修改的部分应用。
- 第三方代码、生成代码和生成区域遵循其原有规则；需要调整生成结果时，应修改对应模板或生成器，不直接修补生成产物。
- 不为满足本规范而进行与当前任务无关的格式化、批量改名或重构，保留用户已有修改。
- 涉及现有 API、序列化字段、反射及 UI 绑定时，优先保持兼容性，不因风格调整改变既有契约。

## 0. 前言

本规范用于统一项目的 C# 编码习惯，使新增和修改的代码清晰、可靠、便于维护。

代码质量关注以下方面：

- 可靠的
- 可维护的
- 高效率的

从长期 ROI（投资回报率）考虑，应在满足功能和性能要求的同时，控制理解、修改和验证代码的成本。

### 0.1 编码规范和最佳实践的目的

通过统一命名、排版和实现约定，降低协作成本。代码审查应结合当前任务检查功能正确性、兼容性和可维护性；具体执行与验证遵循项目 AGENTS.md 和相关技能。

## 1. 命名约定与规范

字段命名遵循项目的[命名与绑定约定](../skills/tengine-dev/references/naming-rules.md)：普通私有字段使用 `_camelCase`；修改既有代码时跟随相邻代码，不批量改名无关字段。

> 注意：
> 在本文档中使用了帕斯卡命名法（Pascal Casing）与驼峰命名法（Camel Casing）。
> **帕斯卡命名法（PascalCase）**：以大写字母开头，后续单词通常以大写字母分隔，例如 `BlackColor`。
> **驼峰命名法（camelCase）**：以小写字母开头，后续单词通常以大写字母分隔，例如 `blackColor`。
> 保留项目已约定的缩写及外部 API 的原始拼写，不机械地将其余字母转成小写。例如 `OnGUI`、`TMP_InputField`、`UniTask` 不应仅为符合命名风格而改写。
> Unity 生命周期回调、接口实现及重写成员沿用原始成员名称，不因通用命名约定改变大小写或拼写。

#### 1.1 使用帕斯卡命名法为类型命名。

类、结构体和枚举使用 PascalCase；接口遵循第 1.4 条的 `I` 前缀约定。

```cs
public class HelloWorld
{
	// do something
}
```

#### 1.2 使用帕斯卡命名法为方法命名。

```cs
public void SayHello(string name)
{
	// do something
}
```

#### 1.3 使用驼峰命名法为方法的参数和方法内的局部变量命名。

```cs
public void SayHello(string name)
{
	string fullMessage = "Hello" + name;

	// do something
}
```

#### 1.4 使用前缀 “I” 结合帕斯卡命名法为接口命名。

```cs
public interface IEntity
{
	// do something
}
```

#### 1.5 使用帕斯卡命名法为属性命名。


#### 1.6 使用具有实际意义的，具有描述性的词语来命名变量。

```cs
Good:

string address;
int salary;


Not Good:

string addr;
int sal;
```

#### 1.7 避免使用含义不明确的单字符变量名。

循环计数器 `i`、`j`，数学表达式或坐标中的 `x`、`y`，以及作用域很短且含义清楚的表达式可以使用惯用的单字符名称；泛型类型参数允许使用 `T`，多个或含义不明确时使用 `TKey`、`TValue` 等描述性名称。跨越较长作用域或表达业务含义时，应使用描述性名称。

```cs
for (int i = 0; i < count; i++)
{
	// do something
}
```

#### 1.8 布尔名称应表达明确的判断语义。

根据含义使用 `Is`、`Has`、`Can`、`Should` 等前缀。参数和局部变量使用 camelCase（如 `isFinished`、`hasTarget`），普通私有字段使用 `_camelCase`（如 `_isFinished`、`_canInteract`），属性和方法使用 PascalCase（如 `IsFinished`、`HasTarget`、`ShouldRetry`）。实现或重写既有接口时保留其名称，不为添加前缀而破坏兼容性。

```cs
private bool _isFinished;

private bool IsTaskFinished()
{
	return _isFinished;
}
```

#### 1.9 文件名应与主要类型名保持一致。

类、接口、结构体和枚举的文件名使用其主要类型名，例如 `HelloWorld.cs`、`IEntity.cs`、`PlayerState.cs`。泛型类型的文件名省略类型参数。

#### 1.10 文件名使用 PascalCase，分部类型允许按职责拆分。

`partial` 类型可以使用 `类型名.职责.cs`，例如 `PlayerController.Input.cs`；主文件仍使用 `类型名.cs`。该约定不要求拆分类型，也不要求批量重命名既有文件；生成文件遵循生成器的命名规则。

## 2. 缩进和空格

#### 2.1 新建自主维护的 C# 文件使用 Tab 缩进，每级一个 Tab，显示宽度为 4 个字符。

修改既有文件时，新增或修改的代码保持相邻代码的缩进方式，不混用 Tab 与空格作为层级缩进，不为统一风格而格式化整个文件。第三方及生成代码遵循“适用范围”中的约定。

本规范的 C# 示例使用 Tab 表示层级缩进；反例中有意展示的错误对齐除外。编辑器应将 Tab 显示宽度设为 4，并保留实际 Tab 字符，避免自动转换为空格。

#### 2.2 注释应该与代码保持列对齐（使用相同的缩进）。

以下示例为类内方法片段，仅演示注释与代码的对齐，不依赖 Unity 或 TEngine API。

正确示例：

```cs
private int CalculateTotal(int unitPrice, int quantity)
{
	// 计算总价。
	int total = unitPrice * quantity;
	return total;
}
```

错误示例（注释与其对应代码的缩进不一致）：

```cs
private int CalculateTotal(int unitPrice, int quantity)
{
	// 计算总价。
		int total = unitPrice * quantity;
		return total;
}
```

#### 2.3 同一级的大括号应该保持列对齐。

```cs
if (isFinished)
{
	// do something
}
```

#### 2.4 使用一个空行来区分代码内的逻辑分组。恰当地运用空格和空行，不要为了“节省篇幅”和“紧凑”而把很多语句挤在一起，而是要多用空格分隔开变量与操作符，用空行分隔开代码块，保持适当地阅读节奏。

以下示例为类内方法片段，仅使用 C# 内置类型及标准字符串 API，演示输入校验、名称整理和结果构造之间的逻辑分组。

正确示例：

```cs
private string FormatGreeting(string name)
{
	if (string.IsNullOrWhiteSpace(name))
	{
		return "Hello";
	}

	string normalizedName = name.Trim();

	return "Hello, " + normalizedName;
}
```

错误示例（省略逻辑分组之间的空行）：

```cs
private string FormatGreeting(string name)
{
	if (string.IsNullOrWhiteSpace(name))
	{
		return "Hello";
	}
	string normalizedName = name.Trim();
	return "Hello, " + normalizedName;
}
```

#### 2.5 在类内每个方法之间应该有且仅有一个空行。

#### 2.6 控制语句的语句体必须使用大括号，大括号单独占行。

`if`、`else`、`else if`、`for`、`foreach`、`while`、`do` 的语句体即使只有一条语句也使用大括号，起止大括号按同一级缩进对齐。

## 3. 良好的编程实践

#### 3.1 方法名应该说明它做了什么。不要使用容易让人误解的名字。

```cs
Good:

public void SavePhoneNumber(string phoneNumber)
{
	// Save the phone number.
}


Not Good:

// This method will save the phone number.
public void SaveDetails(string phoneNumber)
{
	// Save the phone number.
}
```

#### 3.2 方法应围绕单一职责组织，允许流程编排。

入口或流程方法可以协调多个相关步骤，方法名应准确表达整个流程。仅在提升可读性、复用性或可测试性时提取方法，不为满足“只做一件事”而过度拆分，也不把互不相关的职责混入同一方法。

```cs
Good:

private void SaveAddress(string address)
{
	// do something
}

private void SendEmail(string email)
{
	// do something
}

private void SaveAddressAndNotify(string address, string email)
{
	SaveAddress(address);
	SendEmail(email);
}


Not Good:

private void SaveAddress(string address, string email)
{
	// Job 1. Save the address.

	// Job 2. Send an email.
}
```

#### 3.3 总是注意非期望的值。例如，如果你使用了一个参数具有两个可能的值，不要假设如果它不等于其中一个值，就一定等于另一个值。

非法参数、外部输入和预期业务失败应分别处理：非法参数可抛出具体的参数异常；外部输入应先校验并在边界处反馈错误；预期业务失败应通过项目已有的结果或错误反馈机制处理，不应一律抛出异常。下面以方法参数 `memberType` 超出允许范围为例。

```cs
Good:

if (memberType == MemberTypes.Registered)
{
	// Registered user ... do something
}
else if (memberType == MemberTypes.Guest)
{
	// Guest user ... do something 
}
else
{
	throw new ArgumentOutOfRangeException(
		nameof(memberType), memberType, "不支持的成员类型");
}


Not Good:

if (memberType == MemberTypes.Registered)
{
	// Registered user ... do something
}
else
{
	// Guest user ... do something 

	// If we introduce another user type in future, this code will 
	// fail and will not be noticed.
}
```

#### 3.4 避免使用含义不明确的魔法数字。

- 具有业务含义、重复使用或难以理解的数字，应使用具名常量或配置表达；需要随业务调整的值优先使用项目已有的配置机制。
- 常规计数、索引以及含义明显的 `0`、`1` 等值不强制提取；是否提取取决于上下文，不仅取决于数值大小。
- 类型级常量在所属类型内部集中声明，不在类型外或文件头部单独声明。
- 新增常量默认使用 `UPPER_SNAKE_CASE` 命名。该规则仅适用于常量，不套用于所有字段；生成代码遵循生成规则，既有公开接口保持兼容，不为统一命名批量改名。

## 4. 结束语

在满足功能需求和正确性的前提下，保持代码清晰、可维护。通过适合当前任务的代码审查和验证，持续改进代码质量。
