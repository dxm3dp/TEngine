# CUSTOM README	

## 提交规范

格式：`type(scope): 简短说明`

| 前缀     | 用途                                   |
| -------- | -------------------------------------- |
| feat     | 新增功能                               |
| fix      | 修复问题                               |
| refactor | 重构代码，保持原有行为                 |
| perf     | 性能优化                               |
| docs     | 文档修改                               |
| test     | 测试修改                               |
| build    | 构建、依赖或代码生成配置               |
| ci       | 持续集成配置                           |
| chore    | 文件清理、调试配置、生成文件同步等维护 |

示例：

```text
feat(login): 增加登录限流
fix(room): 修复房间人数统计错误
build(proto): 调整网络协议导出设置
chore(debug): 添加调试运行选项
chore(proto): 更新导出的网络协议文件
chore: 删除无用文件
```

---

## 上游仓库同步规范

本项目基于开源仓库 Fork 而来。为了持续跟踪上游项目的更新，同时保持自己的定制开发与上游代码相互独立，采用以下同步方式。

### 1. 远程仓库说明

本地 Git 仓库配置两个 Remote：

| Remote | 指向 | 用途 |
|---|---|---|
| `origin` | 自己的 GitHub Fork 仓库 | 存储和管理自己的代码 |
| `upstream` | 原始开源仓库 | 获取上游最新代码 |

### 2. 同步上游代码

当源仓库的 `main` 分支有更新时，在本地执行以下操作，将上游更新同步到自己的 Fork 仓库。

```bash
# 1. 获取源仓库最新代码
git fetch upstream

# 2. 切换到自己的主分支
git switch main

# 3. 将上游 main 合并到自己的 main
git merge --ff-only upstream/main

# 4. 将更新推送到自己的 Fork
git push origin main
```

**操作说明：**

- `git fetch upstream`：获取上游仓库最新提交，更新本地的 `upstream/main` 引用，不会直接修改当前工作分支。
- `git switch main`：切换到本地 `main` 分支。
- `git merge --ff-only upstream/main`：采用 Fast-forward（快进合并）方式，将上游最新代码同步到本地 `main`。
- `git push origin main`：将同步后的代码推送到自己的 GitHub Fork 仓库。

**注意事项：**

1. 本地 `main` 分支应保持与上游 `main` 分支一致，不在该分支上进行自己的定制开发。
2. 使用 `--ff-only` 可以避免意外产生合并提交。如果本地 `main` 与上游发生分叉，该命令会执行失败，此时应先检查分支差异，不要直接强制覆盖。
3. 执行分支切换前，确保当前工作区没有未提交的修改。
4. 上述操作只同步上游 `main` 分支，不会自动更新自己的 `custom/dev` 等开发分支。

### 3. 将上游更新同步到自己的开发分支

完成上游代码同步后，如果需要让自己的开发分支也获得最新更新，可以执行：

```bash
# 1. 切换到自己的开发分支
git switch custom/dev

# 2. 将最新 main 更新 Rebase 到开发分支
git rebase main
```

如果开发分支之前已经推送到远程仓库，Rebase 完成后需要更新远程分支：

```bash
git push origin custom/dev
```

如果开发分支由多人共同维护，建议优先使用 Merge，避免改写共享分支的提交历史。

