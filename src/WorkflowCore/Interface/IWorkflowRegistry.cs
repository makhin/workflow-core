﻿using WorkflowCore.Models;

namespace WorkflowCore.Interface
{
    public interface IWorkflowRegistry
    {
        void RegisterWorkflow(WorkflowDefinition workflowDefinition);
        void RegisterWorkflow(IWorkflow workflow);
        void RegisterWorkflow<TData>(IWorkflow<TData> workflow) where TData : new();
        WorkflowDefinition GetDefinition(string workflowId, int? version = null);
    }
}
