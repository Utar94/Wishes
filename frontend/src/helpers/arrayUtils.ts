export function orderBy<T>(items: T[], key?: keyof T, isDescending?: boolean): T[] {
  const weight = isDescending ? -1 : 1;
  return key ? [...items].sort((a, b) => (a[key] < b[key] ? -weight : a[key] > b[key] ? weight : 0)) : [...items].sort();
}
