export function cleanParams<T extends object>(params: T): Record<string, string> {
  return Object.fromEntries(
    Object.entries(params).filter(([_, v]) => v !== null && v !== undefined && v !== '')
  );
}
